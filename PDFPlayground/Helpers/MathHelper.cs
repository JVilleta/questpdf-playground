namespace PDFPlayground.Helpers
{
    public static class MathHelper
    {
        public static double RoundToTwo(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}