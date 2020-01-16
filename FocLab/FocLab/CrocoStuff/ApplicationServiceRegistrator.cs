using Croco.Core.Abstractions.Application;
using Croco.Core.Extensions;
using Croco.Core.Implementations.AmbientContext;
using Croco.Core.Implementations.TransactionHandlers;
using System.Threading.Tasks;

namespace CrocoShop.CrocoStuff
{
    public class ApplicationServiceRegistrator
    {
        public static void Register(ICrocoApplication application)
        {
            //Подписка обработчиками сообщений на сообщения
            application.AddMessageHandler<ChatCreatedEvent, ChatCreatedEventHandler>();
            application.AddMessageHandler<ChatRelationUpdatedEvent, ChatRelationUpdatedEventHandler>();

            ChatOptimizations.AddRelationsFromDatabase().GetAwaiter().GetResult();
            LogApplicationInit();
        }

        public static void LogApplicationInit()
        {
            new CrocoTransactionHandler(() => new SystemCrocoAmbientContext()).ExecuteAndCloseTransactionSafe(amb =>
            {
                amb.Logger.LogInfo("App.Initialized", "Приложение инициализировано");

                return Task.CompletedTask;
            }).GetAwaiter().GetResult();
        }
    }
}