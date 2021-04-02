using System.Linq;
using Clt.Contract.Events;
using Clt.Logic;
using Clt.Model;
using Croco.Core.Application;
using Croco.Core.Application.Registrators;
using Croco.Core.Logic.DbContexts;
using Croco.Core.Logic.Files;
using Doc.Logic;
using FocLab.App.Logic.Extensions;
using FocLab.App.Logic.Handlers;
using FocLab.App.Logic.Implementations;
using FocLab.App.Logic.Services.Doc;
using Microsoft.Extensions.DependencyInjection;
using FocLab.Model;
using Tms.Logic;
using Tms.Model;
using FocLab.Logic;

namespace FocLab.App.Logic
{
    public static class LogicRegistrator
    {
        public static void Register(this CrocoApplicationBuilder appBuilder)
        {
            var efRegistrator = new EFCrocoApplicationRegistrator(appBuilder);

            efRegistrator.AddEntityFrameworkDataConnection<CrocoInternalDbContext>();
            appBuilder.RegisterDbFileManager();


            efRegistrator.AddEntityFrameworkDataConnection<FocLabDbContext>();
            FocLabLogicRegistrator.Register(appBuilder);
            
            efRegistrator.AddEntityFrameworkDataConnection<TmsDbContext>();
            TmsRegistrator.Register<TmsUsersStorage>(appBuilder, MyIdentityExtensions.IsAdmin);

            efRegistrator.AddEntityFrameworkDataConnection<CltDbContext>();
            CltLogicRegistrator.Register(appBuilder);

            var services = appBuilder.Services;

            DocumentRegistrator.Register(services);

            services.AddScoped<ChemistryExperimentDocumentProccessor>();
            services.AddScoped<ChemistryTaskDocumentProccessor>();

            RegisterFocLabWorkerTypes(services);

            AddMessageHandlers(appBuilder);
        }

        private static void AddMessageHandlers(CrocoApplicationBuilder appBuilder)
        {
            var evSourceOpts = appBuilder.EventSourceOptions;

            evSourceOpts
                .AddMessageHandler<ClientRegisteredEvent, ClientRegisteredEventHandler>(true)
                .AddMessageHandler<ClientDataUpdatedEvent, ClientDataUpdatedEventHandler>(true);
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