using Doc.Logic.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Logic
{
    public static class DocumentRegistrator
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddSingleton<DocumentProccessorBase>();
            services.AddTransient<ChemistryTaskDocumentProccessor>();
            services.AddTransient<ChemistryExperimentDocumentProccessor>();
        }
    }
}