using QuestPDF.Elements.Table;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PDFPlayground.Styles
{
    public static class PdfTable
    {
        public static void StyleTableHeader(TableCellDescriptor table, string text, Action<TextBlockDescriptor>? customStyle = null)
        {
            var textCell = table.Cell().ExtendHorizontal()
                .BorderHorizontal(4, QuestPDF.Infrastructure.Unit.Mil)
                .PaddingVertical(5)
                .PaddingHorizontal(3)
                .Text(text)
                .FontColor(PdfColors.Primary)
                .Bold();

            customStyle?.Invoke(textCell);
        }

        public static IContainer StyleTableCell(TableDescriptor table, string backgroundColor, bool leftRounded = false, bool rightRounded = false)
        {

            IContainer cell = table.Cell().Background(backgroundColor);

            if(leftRounded)
                cell = cell.CornerRadiusBottomLeft(6).CornerRadiusTopLeft(6);
            
            if(rightRounded)
                cell = cell.CornerRadiusBottomRight(6).CornerRadiusTopRight(6);
            
            return cell.Padding(5);
        }
    }
}