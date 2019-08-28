using System;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Settings.Statics;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
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
        public async Task<BaseApiResponse> UpdateTaskAsync(UpdateTaskAsPerformer model)
        {
            if(!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы в системе");
            }

            var validation = ValidateModel(model);

            if(!validation.IsSucceeded)
            {
                return validation;
            }

            var repo = GetRepository<ChemistryTask>();

            var chemistryTask = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if(chemistryTask == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            var userId = UserId;

            if(chemistryTask.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем этого задания");
            }

            chemistryTask.PerformerQuality = model.PerformerQuality;
            chemistryTask.PerformerQuantity = model.PerformerQuantity;
            chemistryTask.PerformerText = model.PerformerText;
            chemistryTask.SubstanceCounterJson = model.SubstanceCounterJson;

            repo.UpdateHandled(chemistryTask);

            return await TrySaveChangesAndReturnResultAsync("Характеристики задачи обновлены");
        }

        /// <summary>
        /// Выполнить задание
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mailSender"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> PerformTaskAsync(PerformTaskModel model, IUserMailSender mailSender)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var repo = GetRepository<ChemistryTask>();

            var task = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.TaskId);

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
        public PerformerChemistryTasksWorker(ICrocoAmbientContext) : base(contextWrapper)
        {
        }
    }
}