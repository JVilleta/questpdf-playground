using PDFPlayground.Contracts;
using PDFPlayground.Dtos;
using PDFPlayground.Extensions;
using PDFPlayground.Styles;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDFPlayground.Engines
{
    public class FileBuilderEngine : IFileBuilderEngine
    {
        private static string FormatValue(object? value)
        {
            if (value == null)
                return "";

            var type = value.GetType();

            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            if (type.IsEnum)
            {
                var enumIntValue = (int)Convert.ChangeType(value, typeof(int));
                return enumIntValue.GetEnumDescription(type);
            }

            return value.ToString()!;
        }
        public Document CreateDocument<T>(List<HeaderFieldDto<T>>? fieldsHeader, T dataHeader, LocationDto locationData, DocumentLayoutDto element, int columns = 2)
        {
            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Element(Header);
                    page.Content().Element(element.Content!);
                    if(element.Footer is not null)
                        page.Footer().Element(element.Footer);

                    void Header(IContainer container)
                    {
                        container.Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    table.Cell().Column(contactColumn =>
                                    {
                                        if (locationData.LogoImg is not null)
                                        {
                                            contactColumn.Item()
                                                .Height(60).Width(60)
                                                .Image(locationData.LogoImg);
                                        }
                                        contactColumn.Item().Text(locationData.Name).FontColor(PdfColors.Primary).FontSize(13).Bold();
                                        contactColumn.Item().Text($"RNC: {locationData.RNC}");
                                        contactColumn.Item().Text(locationData.Address);
                                        contactColumn.Item().Text(locationData.PhoneNumber);
                                        contactColumn.Item().Text(locationData.Email);
                                    });

                                    table.Cell().ShowIf(element.HeaderSecondColumn is not null).Element(element.HeaderSecondColumn!);

                                    table.Cell().Column(logoColumn =>
                                    {
                                        if (element.QRCode is not null)
                                        {
                                            logoColumn.Item()
                                                .Height(100)
                                                .AlignRight()
                                                .Image(element.QRCode)
                                                .FitArea();
                                        }
                                    });
                                });
                            });

                            column.Item().CLineHorizontal(15);

                            if(fieldsHeader is not null){

                                column.Item().Row(row =>
                                {
                                    row.Spacing(30);

                                    var itemsPerColumn = (int)Math.Ceiling((decimal)fieldsHeader.Count / columns);

                                    for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                                    {
                                        row.RelativeItem().Column(col =>
                                        {
                                            var startPosition = itemsPerColumn * columnIndex;

                                            for (int rowIndex = 0; rowIndex < itemsPerColumn; rowIndex++)
                                            {
                                                var position = startPosition + rowIndex;

                                                if (fieldsHeader.Count == position)
                                                    break;

                                                var field = fieldsHeader[position];

                                                var rawValue = field.ValueSelector(dataHeader);

                                                var value = field.Formatter?.Invoke(rawValue) ?? FormatValue(rawValue) ?? "";

                                                col.Item().Text(text =>
                                                {
                                                    text.Span($"{field.Label}: ").Bold();
                                                    text.Span(value);
                                                });
                                                col.Item().CLineHorizontal();
                                            }
                                        });
                                    }

                                    column.Item().PaddingTop(10).CLineHorizontal();

                                });
                            }
                        });
                    }
                });
            });
        }

    }
}