using System.Threading.Tasks;
using FocLab.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace FocLab.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var logger = app.ApplicationServices.GetRequiredService<ILoggerManager>();
                        logger.LogException(contextFeature.Error);
                    }
                    return Task.CompletedTask;
                });
            });
        }
    }
}