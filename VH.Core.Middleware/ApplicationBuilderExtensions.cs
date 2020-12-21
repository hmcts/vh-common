using Microsoft.AspNetCore.Builder;
using VH.Core.Middleware.Exception;

namespace VH.Core.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add exception middleware to the pipeline. This must be added in the correct order. Please see Microsoft documentation for more details.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
