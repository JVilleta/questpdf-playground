using PDFPlayground.Dtos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PDFPlayground.Mocks
{
    public static class MockData
    {
        public static LocationDto Location() => new()
        {
            Name = "Residencial Villeta",
            RNC = "1-01-00000-0",
            Address = "Av. Villa Mira #123, Santo Domingo",
            PhoneNumber = "(809) 555-0101",
            Email = "residencialvillamira@gmail.com",
            LogoImg = null
        };
    }
}
