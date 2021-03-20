using Croco.Core.Application;
using Doc.Contract.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Logic.RegistrationModule
{
    public static class DocServiceRegistrator
    {
        public static void Register(CrocoApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .Services
                .AddScoped<IDocumentService, DocumentService>();
        }
    }
}