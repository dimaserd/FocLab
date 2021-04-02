using Croco.Core.Application;
using Croco.Core.Application.Registrators;
using Croco.Core.Logic.Files;
using Croco.Core.Logic.Files.Events;
using Microsoft.Extensions.DependencyInjection;
using FocLab.Logic.Commands;
using FocLab.Logic.EventHandlers;
using FocLab.Logic.Services;
using FocLab.Model;
using System.Linq;

namespace FocLab.Logic
{
    public static class FocLabLogicRegistrator
    {
        public static void Register(CrocoApplicationBuilder appBuilder)
        {
            Check(appBuilder);

            AddMessageHandlers(appBuilder);

            RegisterFocLabSerivceTypes(appBuilder.Services);
        }

        private static void AddMessageHandlers(CrocoApplicationBuilder appBuilder)
        {
            var eventSourceOptions = appBuilder.EventSourceOptions;

            eventSourceOptions
                .AddMessageHandler<CreateUserCommand, CreateUserCommandHandler>()
                .AddMessageHandler<UpdateUserCommand, UpdateUserCommandHandler>()
                .AddMessageHandler<FilesUploadedEvent, FilesUploadedEventHandler>(true);
        }

        private static void RegisterFocLabSerivceTypes(IServiceCollection services)
        {
            var baseType = typeof(FocLabService);

            var typesToRegister = baseType
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract);

            foreach (var typeToRegister in typesToRegister)
            {
                services.AddScoped(typeToRegister);
            }
        }

        private static void Check(CrocoApplicationBuilder appBuilder)
        {
            new EFCrocoApplicationRegistrator(appBuilder).CheckForEFDataCoonection<FocLabDbContext>();
            DbFileManagerServiceCollectionExtensions.CheckForDbFileManager(appBuilder.Services);
        }
    }
}