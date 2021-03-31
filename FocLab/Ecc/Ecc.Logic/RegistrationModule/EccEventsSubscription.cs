using Croco.Core.Application;
using Ecc.Logic.EventHandlers;
using NewFocLab.Logic.Events;

namespace Ecc.Logic.RegistrationModule
{
    public static class EccEventsSubscription
    {
        public static void Subscribe(CrocoApplicationBuilder builder)
        {
            //Подписка обработчиками событий на события
            builder.EventSourceOptions
                .AddMessageHandler<ExperimentPerformedEvent, ExperimentPerformedEventHandler>();
        }
    }
}