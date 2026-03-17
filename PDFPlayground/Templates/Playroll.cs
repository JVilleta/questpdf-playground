using PDFPlayground.Contracts;
using PDFPlayground.Dtos;
using PDFPlayground.Extensions;
using PDFPlayground.Helpers;
using PDFPlayground.Mocks;
using PDFPlayground.Styles;
using QuestPDF.Elements.Table;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFPlayground.Templates
{
    public class PlayrollTemplate(IBusinessEngineFactory businessEngineFactory)
    {
        private static readonly List<HeaderFieldDto<PlayrollDto>> fieldsHeader = new()
        {
            new () { Label = "Empleado", ValueSelector = (playroll) =>  $"{playroll.EmployeeName} ({playroll.EmployeeCode})", },
            new () { Label = "Identificación", ValueSelector = (playroll) => playroll.EmployeeIdentification },
            new () { Label = "Cargo", ValueSelector = (playroll) => playroll.EmployeePosition },
            new () { Label = "Departamento", ValueSelector = (playroll) => playroll.EmployeeDepartment },
            new()
            {
                Label = "Periodo",
                ValueSelector = p => (p.EmployeePaymentStartDate, p.EmployeePaymentEndDate),
                Formatter = v =>
                {
                    var (start, end) = ((DateTime, DateTime))v!;
                    return $"{start:dd/MM/yyyy} - {end:dd/MM/yyyy}";
                }
            },            new () { Label = "Frecuencia de pago", ValueSelector = (playroll) => playroll.PaymentFrequency },
            new () { Label = "Fecha de pago", ValueSelector = (playroll) => playroll.PaymentDate },
            new () { Label = "Método de pago", ValueSelector = (playroll) => playroll.PaymentMethod },
        };
        public void BuildDocument(PlayrollDto playroll)
        {
            var fileBuilderEngine = businessEngineFactory.Get<IFileBuilderEngine>();
            var location = MockData.Location();

            var layout = new DocumentLayoutDto()
            {
                HeaderSecondColumn = container =>
                {
                    container.AlignMiddle().Column(col =>
                    {
                        col.Item().AlignCenter().Text("Nómina").Bold().FontSize(14).FontColor(PdfColors.Primary);
                        col.Item().AlignCenter().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });
                },
                Content = container =>
                {
                    container.PaddingTop(10).Column(col =>
                    {
                        col.Spacing(6);
                        col.Item().Text("Detalle de nómina").FontSize(12).Bold();
                        col.Item().Text("Aquí puedes renderizar tablas, totales, etc. usando QuestPDF.");

                        var totalEarnings = MathHelper.RoundToTwo(playroll.PlayrollDetails.Sum(x => x.Amount));
                        var totalDeductions = MathHelper.RoundToTwo(playroll.PlayrollDeductions.Sum(x => x.Amount));
                        var totalContributions = MathHelper.RoundToTwo(playroll.PlayrollContributions.Sum(x => x.Amount));

                        col.Item().PaddingTop(10).Table(table =>
                        {

                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                PdfTable.StyleTableHeader(header, "Devengado");
                                PdfTable.StyleTableHeader(header, "Detalle");
                                PdfTable.StyleTableHeader(header, "Monto");
                            });

                            foreach (var (detail, i) in playroll.PlayrollDetails.Select((detail, i) => (detail, i)))
                            {
                                var isEvenRow = i % 2 == 0;
                                var backgroundColor = isEvenRow ? PdfColors.White : PdfColors.LightGray;

                                PdfTable.StyleTableCell(table, backgroundColor, true).Text(detail.Name);
                                PdfTable.StyleTableCell(table, backgroundColor).Text(detail.Note);
                                PdfTable.StyleTableCell(table, backgroundColor, false, true).Text(detail.Amount.FormatCurrency());
                            }

                            if (playroll.PlayrollDetails.Count > 0)
                            {
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary, true).Text("Total de devengados").Bold().FontColor(PdfColors.Primary);
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary).Text(string.Empty);
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary, false, true).Text(totalEarnings.FormatCurrency()).Bold().FontColor(PdfColors.Primary);
                            }
                        });

                        col.Item().PaddingTop(10).Table(table =>
                        {

                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                PdfTable.StyleTableHeader(header, "Deducciones");
                                PdfTable.StyleTableHeader(header, "Base/Nota");
                                PdfTable.StyleTableHeader(header, "Monto");
                            });

                            foreach (var (deduction, i) in playroll.PlayrollDeductions.Select((deduction, i) => (deduction, i)))
                            {
                                var isEvenRow = i % 2 == 0;
                                var backgroundColor = isEvenRow ? PdfColors.White : PdfColors.LightGray;

                                PdfTable.StyleTableCell(table, backgroundColor, true).Text(deduction.Name);
                                PdfTable.StyleTableCell(table, backgroundColor).Text(deduction.Note);
                                PdfTable.StyleTableCell(table, backgroundColor, false, true).Text(deduction.Amount.FormatCurrency());
                            }

                            if (playroll.PlayrollDetails.Count > 0)
                            {
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary, true).Text("Total de deductions").Bold().FontColor(PdfColors.Primary);
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary).Text(string.Empty);
                                PdfTable.StyleTableCell(table, PdfColors.LightPrimary, false, true).Text(totalDeductions.FormatCurrency()).Bold().FontColor(PdfColors.Primary);
                            }
                        });

                        col.Item().PaddingTop(10)
                        .Shadow(new BoxShadowStyle
                        {
                            Color = Colors.Grey.Lighten1,
                            Blur = 0,
                            OffsetX = 3,
                            OffsetY = 3
                        })
                        .CornerRadius(8)
                        .Background(PdfColors.LightPrimary).Row(row =>
                        {
                            row.RelativeItem().Padding(10).Text("Neto a pagar").FontSize(12).FontColor(PdfColors.Primary).Bold();
                            row.RelativeItem().Padding(10).AlignRight().Text((totalEarnings - totalDeductions).FormatCurrency()).FontSize(12).FontColor(PdfColors.Primary).Bold();
                        });

                        col.Item().PaddingTop(10).Text("Aportes del empleador (regerencial)").FontSize(12).Bold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                PdfTable.StyleTableHeader(header, "Concepto");
                                PdfTable.StyleTableHeader(header, "Tasa");
                                PdfTable.StyleTableHeader(header, "Monto");
                            });

                            foreach (var (contribution, i) in playroll.PlayrollContributions.Select((contribution, i) => (contribution, i)))
                            {
                                var isEvenRow = i % 2 == 0;
                                var backgroundColor = isEvenRow ? PdfColors.White : PdfColors.LightGray;

                                PdfTable.StyleTableCell(table, backgroundColor, true).Text(contribution.Name);
                                PdfTable.StyleTableCell(table, backgroundColor).Text(contribution.Tasa.ToString("P2"));
                                PdfTable.StyleTableCell(table, backgroundColor, false, true).Text(contribution.Amount.FormatCurrency());
                            }

                            if(playroll.PlayrollContributions.Count > 0)
                            {
                                col.Item().PaddingTop(-5)
                                .CornerRadius(8)
                                .Background(PdfColors.LightPrimary).Column(col =>
                                {
                                    col.Item().PaddingHorizontal(5).PaddingVertical(5).Row(row =>
                                    {
                                        row.ConstantItem(370).Text("Costo Total empleador").FontColor(PdfColors.Primary).Bold();
                                        row.RelativeItem().Text(totalContributions.FormatCurrency()).FontColor(PdfColors.Primary).Bold();
                                    });
                                    col.Item().PaddingHorizontal(5).PaddingBottom(5).Row(row =>
                                    {
                                        row.ConstantItem(370).Text("Total de aportes").FontColor(PdfColors.Primary).Bold();
                                        row.RelativeItem().Text(totalContributions.FormatCurrency()).FontColor(PdfColors.Primary).Bold();
                                    });
                                });
                            }
                        });

                        
                    });
                   
                },
                Footer = container =>
                {
                    container.AlignCenter().Text(text =>
                    {
                        text.Span("Documento de prueba - ");
                        text.Span("QuestPDF Playground").Bold();
                    });
                }
            };
            var document = fileBuilderEngine.CreateDocument(fieldsHeader: fieldsHeader, dataHeader: playroll, locationData: location, element: layout);

            document.GeneratePdfAndShow();
        }
    }
}