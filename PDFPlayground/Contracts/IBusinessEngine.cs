namespace PDFPlayground.Contracts
{
    public interface IBusinessEngine
    {
    }

    public interface IBusinessEngine<T> : IBusinessEngine
        where T : class, new()
    {
    }
}