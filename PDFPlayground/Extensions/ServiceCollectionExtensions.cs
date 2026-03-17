using Microsoft.Extensions.DependencyInjection;
using PDFPlayground.Contracts;
using PDFPlayground.Engines;

namespace PDFPlayground.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessEngineServices(this IServiceCollection services)
        {
            services.AddScoped<IBusinessEngineFactory, BusinessEngineFactory>();

            // Engines injection
            var engineTypes =
                typeof(BusinessEngineFactory).Assembly
                                             .ExportedTypes
                                             .Where(x => typeof(IBusinessEngine).IsAssignableFrom(x) &&
                                                         !x.IsInterface &&
                                                         !x.IsAbstract).ToList();

            engineTypes.ForEach(engineType =>
            {
                services.AddScoped(engineType.GetInterface($"I{engineType.Name}")!,
                                   engineType);
            });

            return services;
        }
    }
}
