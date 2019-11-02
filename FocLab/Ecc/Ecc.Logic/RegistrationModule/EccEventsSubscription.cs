using Croco.Core.Abstractions.EventSourcing;
using Ecc.Logic.EventHandlers;
using FocLab.Logic.Events;

namespace Ecc.Logic.RegistrationModule
{
    public static class EccEventsSubscription
    {
        public static void Subscribe(ICrocoMessageListener evListener)
        {
            //Подписка обработчиками событий на события
            evListener.AddMessageHandler<ExperimentPerformedEvent, ExperimentPerformedEventHandler>();
        }
    }
}
