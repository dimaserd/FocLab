using Croco.Core.Abstractions;
using Croco.Core.EventSourcing.Abstractions;
using Croco.Core.EventSourcing.Enumerations;
using System.Threading.Tasks;

namespace FocLab.Implementations
{
    public class ApplicationCrocoEventSourcer : ICrocoEventSourcer
    {
        public ApplicationCrocoEventSourcer(ICrocoMessagePublisher eventPublisher)
        {
            Publisher = eventPublisher;
        }

        public ICrocoMessagePublisher Publisher { get; }

        public Task UpdateEventHandlerStateAsync(ICrocoAmbientContext ambientContext, CrocoMessageState state)
        {
            ambientContext.Logger.LogInfo($"Обработчик события завершил работу. Статус {state}");
            return Task.CompletedTask;
        }
    }
}
