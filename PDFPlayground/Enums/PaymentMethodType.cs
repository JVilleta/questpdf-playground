using System.ComponentModel;

namespace PDFPlayground.Enums
{
    public enum PaymentMethodType
    {
        [Description("Cash")]
        Cash = 20,
        [Description("Transfer")]
        Transfer = 21,
        [Description("Check")]
        Check = 22,
        [Description("Card")]
        Card = 23,
        [Description("Fund")]
        Fund = 24,
    }
}