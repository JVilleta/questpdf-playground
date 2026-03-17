using QuestPDF.Infrastructure;

namespace PDFPlayground.Styles
{
    public static class PdfColors
    {
        public static readonly Color Black = new(0xFF000000);
        public static readonly Color Dark = new(4285887861u);
        public static readonly Color Gray = new(0xFF757575);
        public static readonly Color Success = new(0xFF37D1DA);
        public static readonly Color Primary = new(0xFF001B5E);
        public static readonly Color LightPrimary = new(0xFFBCDEFE);
        public static readonly Color Warning = new(0xFFFFAE1F);
        public static readonly Color LightGray = new(4293848814u);
        public static readonly Color TransparentGray = new(0x80C8C8C8u);
        public static readonly Color White = new(uint.MaxValue);
    }
}