using System;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using NewFocLab.Logic.Events;
using NewFocLab.Model.Entities;

namespace NewFocLab.Logic.Services.ChemistryTasks
{
    /// <summary>
    /// Методы исполнителя для работы с химическими заданиями
    /// </summary>
    public class PerformerChemistryTasksWorker : FocLabService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"></param>
        /// <param name="application"></param>
        public PerformerChemistryTasksWorker(ICrocoAmbientContextAccessor context,
            ICrocoApplication application) : base(context, application)
        {
        }

        /// <summary>
        /// Обновить задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UpdateTaskAsync(UpdateTaskAsPerformer model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы в системе");
            }

            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return validation;
            }

            var repo = GetRepository<ChemistryTask>();

            var chemistryTask = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (chemistryTask == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            var userId = UserId;

            if (chemistryTask.PerformerUserId != userId)
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
        /// <returns></returns>
        public async Task<BaseApiResponse> PerformTaskAsync(PerformTaskModel model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var repo = GetRepository<ChemistryTask>();

            var task = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if (task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            var userId = UserId;

            if (task.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем данного задания");
            }

            if (task.PerformedDate.HasValue && model.IsPerformed)
            {
                return new BaseApiResponse(false, "Задание уже является выполненным");
            }

            if (!task.PerformedDate.HasValue && !model.IsPerformed)
            {
                return new BaseApiResponse(false, "Задание уже является отмененным");
            }

            if (!task.PerformedDate.HasValue)
            {
                task.PerformedDate = DateTime.Now;

                repo.UpdateHandled(task);

                await SaveChangesAsync();

                await PublishMessageAsync(new ChemistryTaskPerformedEvent
                {
                    Id = task.Id
                });

                return new BaseApiResponse(true, "Вы пометили данное задание как выполненное");
            }

            task.PerformedDate = null;

            repo.UpdateHandled(task);

            return await TrySaveChangesAndReturnResultAsync("Вы пометили данное задание как не выполненное");
        }
    }
}