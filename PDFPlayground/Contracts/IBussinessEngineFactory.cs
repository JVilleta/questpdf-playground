namespace PDFPlayground.Contracts
{
    public interface IBusinessEngineFactory
    {
        T Get<T>() where T : IBusinessEngine;
    }
}