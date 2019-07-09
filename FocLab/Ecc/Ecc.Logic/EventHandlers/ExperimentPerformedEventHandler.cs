﻿using System.Threading.Tasks;
using Croco.Core.EventSource;
using FocLab.Logic.Events;

namespace Ecc.Logic.EventHandlers
{
    public class ExperimentPerformedEventHandler : AbstractEventHandler<ExperimentPerformedEvent>
    {
        public override Task HandleEvent(ExperimentPerformedEvent model)
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