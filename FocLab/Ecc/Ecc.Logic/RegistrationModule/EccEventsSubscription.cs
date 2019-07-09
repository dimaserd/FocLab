using Croco.Core.EventSource.Abstractions;
using Ecc.Logic.EventHandlers;
using FocLab.Logic.Events;

namespace Ecc.Logic.RegistrationModule
{
    public static class EccEventsSubscription
    {
        public static void Subscribe(ICrocoEventListener evListener)
        {
            //Подписка обработчиками событий на события
            //evListener.AddEventHandler<ExperimentPerformedEvent, ExperimentPerformedEventHandler>();
        }
    }
}
