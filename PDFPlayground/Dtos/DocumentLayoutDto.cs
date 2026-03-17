using QuestPDF.Infrastructure;

namespace PDFPlayground.Dtos{
    public class DocumentLayoutDto{
        public Action<IContainer>? HeaderSecondColumn { get; set; }
        public Action<IContainer>? Content { get; set; }
        public Action<IContainer>? Footer{ get; set; }
        public byte[]? QRCode { get; set; }
    }
}