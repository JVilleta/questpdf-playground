using System.Globalization;

namespace PDFPlayground.Extensions
{
    public static class DoubleExtensions
    {
        public static string FormatCurrency(this double amount, string currencyCode = "DOP")
        {
            if (double.IsNaN(amount))
                amount = 0;

            var formattedAmount = amount.ToString("N2", CultureInfo.InvariantCulture);

            return $"{currencyCode}{formattedAmount}";
        }

        public static string FormatCurrency(this double? amount, string currencyCode = "DOP")
        {
            return FormatCurrency(amount ?? 0, currencyCode);
        }
    }
}