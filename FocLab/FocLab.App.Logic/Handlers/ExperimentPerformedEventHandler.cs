using System.Threading.Tasks;
using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Microsoft.Extensions.Logging;
using FocLab.Logic.Events;

namespace FocLab.App.Logic.Handlers
{
    public class ExperimentPerformedEventHandler : CrocoMessageHandler<ExperimentPerformedEvent>
    {
        public ExperimentPerformedEventHandler(ICrocoApplication application, ILogger<ExperimentPerformedEventHandler> logger) : base(application, logger)
        {
        }

        public override Task HandleMessage(ExperimentPerformedEvent model)
        {
            //await mailSender.SendMailUnSafeAsync(new SendMailMessage
            //{
            //    Body = $"<p>Пользователь {userMe.Email} завершил <a href='{domainName}/Chemistry/Experiments/Experiment/{experiment.Id}'>эксперимент</a>.</p>",
            //    UserId = user.Id,
            //    Subject = "Эксперимент завершен"
            //});

            return Task.CompletedTask;
        }
    }
}