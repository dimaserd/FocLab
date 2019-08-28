using Croco.Core.Abstractions;
using Croco.Core.EventSourcing.Abstractions;
using Croco.Core.EventSourcing.Enumerations;
using System.Threading.Tasks;

namespace FocLab.Implementations
{
    public class ApplicationCrocoEventSourcer : ICrocoEventSourcer
    {
        public ApplicationCrocoEventSourcer(ICrocoEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher;
        }

        public ICrocoEventPublisher EventPublisher { get; }

        public Task UpdateEventHandlerStateAsync(ICrocoAmbientContext ambientContext, EventHandleState state)
        {
            ambientContext.Logger.LogInfo($"Обработчик события завершил работу. Статус {state}");
            return Task.CompletedTask;
        }
    }
}
