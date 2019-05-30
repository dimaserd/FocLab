using System;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Implementations;
using FocLab.Logic.Settings;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryTasks
{
    /// <summary>
    /// Методы исполнителя для работы с химическими заданиями
    /// </summary>
    public class PerformerChemistryTasksWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Обновить задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UpdateTaskAsync(ChemistryTaskDto model)
        {
            if(!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы в системе");
            }

            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var chemistryTask = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if(chemistryTask == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            var userId = UserId;

            if(chemistryTask.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем этого задания");
            }

            Context.Entry(chemistryTask).State = EntityState.Modified;

            chemistryTask.PerformerQuality = model.PerformerQuality;
            chemistryTask.PerformerQuantity = model.PerformerQuantity;
            chemistryTask.PerformerText = model.PerformerText;
            chemistryTask.SubstanceCounterJson = model.SubstanceCounterJson;

            await Context.SaveChangesAsync();

            return new BaseApiResponse(true, "Характеристики задачи обновлены");
        }

        /// <summary>
        /// Выполнить задание
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Context"></param>
        /// <param name="mailSender"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> PerformTaskAsync(PerformTaskModel model, IUserMailSender mailSender)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var task = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if(task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            var userId = UserId;

            if(task.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем данного задания");
            }

            if(task.PerformedDate.HasValue && model.IsPerformed)
            {
                return new BaseApiResponse(false, "Задание уже является выполненным");
            }

            if (!task.PerformedDate.HasValue && !model.IsPerformed)
            {
                return new BaseApiResponse(false, "Задание уже является отмененным");
            }

            Context.Entry(task).State = EntityState.Modified;

            if (!task.PerformedDate.HasValue)
            {
                
                task.PerformedDate = DateTime.Now;

                await Context.SaveChangesAsync();
                
                var domainName = ((FocLabWebApplication)CrocoApp.Application).DomainName;

                var searcher = new UserSearcher(ApplicationContextWrapper);

                var user = await searcher.GetUserByEmailAsync(ChemistryAdminSettings.AdminEmail);

                var myId = UserId;

                var meUser = await Context.Users.FirstOrDefaultAsync(x => x.Id == myId);

                await mailSender.SendMailUnSafeAsync(new SendMailMessage
                {
                    Body = $"<p>Пользователь {meUser.Email} завершил <a href='{domainName}/Chemistry/Chemistry/Task/{task.Id}'>задание</a>.</p>",
                    UserId = user.Id,
                    Subject = "Задание завершено"
                });
                
                return new BaseApiResponse(true, "Вы пометили данное задание как выполненное");
            }

            task.PerformedDate = null;

            await Context.SaveChangesAsync();

            return new BaseApiResponse(true, "Вы пометили данное задание как не выполненное");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public PerformerChemistryTasksWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}