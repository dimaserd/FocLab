using System.Linq;
using Croco.Core.Application;
using Croco.Core.Application.Registrators;
using Doc.Logic;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Services.Doc;
using Microsoft.Extensions.DependencyInjection;
using NewFocLab.Logic;
using NewFocLab.Model;
using Tms.Logic;
using Tms.Model;

namespace FocLab.Logic
{
    public static class LogicRegistrator
    {
        public static void Register(this CrocoApplicationBuilder appBuilder)
        {
            var efRegistrator = new EFCrocoApplicationRegistrator(appBuilder);
            efRegistrator.AddEntityFrameworkDataConnection<FocLabDbContext>();
            FocLabLogicRegistrator.Register(appBuilder);
            efRegistrator.AddEntityFrameworkDataConnection<TmsDbContext>();
            TmsRegistrator.Register<TmsUsersStorage>(appBuilder, MyIdentityExtensions.IsAdmin);

            var services = appBuilder.Services;

            DocumentRegistrator.Register(services);

            services.AddScoped<ChemistryExperimentDocumentProccessor>();
            services.AddScoped<ChemistryTaskDocumentProccessor>();

            RegisterFocLabWorkerTypes(services);
        }

        private static void RegisterFocLabWorkerTypes(IServiceCollection services)
        {
            var focLabWorkerTypes = typeof(FocLabWorker)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(FocLabWorker)) && !t.IsAbstract)
                .ToList();

            foreach (var focLabWorkerType in focLabWorkerTypes)
            {
                services.AddTransient(focLabWorkerType);
            }
        }
    }
}