using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using PDFPlayground.Styles;

namespace PDFPlayground.Extensions
{
    public static class QuestPdfExtensions
    {

        public static void CLineHorizontal(this IContainer container, int paddingY = 5, int width = 4, Color? color = null)
        {
            container.PaddingVertical(paddingY)
                     .LineHorizontal(width, Unit.Mil)
                     .LineColor(color ?? PdfColors.Gray);
        }

        public static IContainer CBorderBottom(this IContainer container, float height = 4)
        {
            return container.BorderBottom(height, Unit.Mil);
        }

        public static IContainer CBorder(this IContainer container, float height = 4)
        {
            return container.Border(height, Unit.Mil)
                            .BorderColor(PdfColors.Gray);
        }
    }
}