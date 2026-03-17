using PDFPlayground.Dtos;
using QuestPDF.Fluent;

namespace PDFPlayground.Contracts
{
    public interface IFileBuilderEngine : IBusinessEngine
    {
        Document CreateDocument<T>(List<HeaderFieldDto<T>>? fieldsHeader, T dataHeader, LocationDto locationData, DocumentLayoutDto element, int columns = 2);
    }
}
