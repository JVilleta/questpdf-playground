using Microsoft.Extensions.DependencyInjection;
using PDFPlayground.Contracts;

namespace PDFPlayground.Engines
{
    public class BusinessEngineFactory(IServiceProvider serviceProvider) : IBusinessEngineFactory
    {
        public T Get<T>() where T : IBusinessEngine
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
