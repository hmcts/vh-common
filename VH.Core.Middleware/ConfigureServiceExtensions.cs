using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using VH.Core.Middleware.Logging;

namespace VH.Core.Middleware
{
    public static class ConfigureServiceExtensions
    {
        /// <summary>
        /// Add Logging Middleware. This should be the first filter registered.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoggingMiddlewareFilter(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoggingDataExtractor, LoggingDataExtractor>();
            serviceCollection.AddMvc(opt => opt.Filters.Add(typeof(LoggingMiddleware))).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            return serviceCollection;
        }
    }
}
