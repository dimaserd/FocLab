using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Common.Models;
using Croco.Core.Abstractions;
using Croco.Core.Data.Abstractions.Repository;
using FocLab.Logic.Models.Reagents;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryReagents
{
    /// <summary>
    /// Класс обеспечивающий взаимодействие с сущностью реагенты
    /// </summary>
    public class ChemistryReagentsWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Получить реагенты
        /// </summary>
        /// <returns></returns>
        public Task<List<ChemistryReagentNameAndIdModel>> GetReagentsAsync()
        {
            return GetRepository<ChemistryReagent>().Query()
                .Select(ChemistryReagentNameAndIdModel.SelectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Получить реагенты
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ChemistryReagentModel> GetReagentAsync(string id)
        {
            return GetRepository<ChemistryReagent>().Query()
                .Select(ChemistryReagentModel.SelectExpression)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        /// <summary>
        /// Создание реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateOrUpdateReagentAsync(ChemistryReagentNameAndIdModel model)
        {
            if(!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            model.Name = model.Name.Trim();

            var repo = GetRepository<ChemistryReagent>();

            if(!string.IsNullOrWhiteSpace(model.Id))
            {
                return await EditReagentAsync(model, repo);
            }

            if(await repo.Query().AnyAsync(x => x.Name == model.Name))
            {
                return new BaseApiResponse(false, "Реагент с данным именем уже существует");
            }

            repo.CreateHandled(new ChemistryReagent
            {
                Name = model.Name,
            });

            return await TrySaveChangesAndReturnResultAsync("Реагент создан");
        }


        /// <summary>
        /// Редактирование реагента
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private async Task<BaseApiResponse> EditReagentAsync(ChemistryReagentNameAndIdModel model, IRepository<ChemistryReagent> repo)
        {
            if (await repo.Query().AnyAsync(x => x.Name == model.Name && x.Id != model.Id)) 
            {
                return new BaseApiResponse(false, "Реагент с данным именем уже существует");
            }

            var chemistryReagent = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if(chemistryReagent == null)
            {
                return new BaseApiResponse(false, "Реагент не найден по указанному идентификатору");
            }

            chemistryReagent.Name = model.Name;

            repo.UpdateHandled(chemistryReagent);

            return await TrySaveChangesAndReturnResultAsync("Реагент отредактирован");
        }


        /// <summary>
        /// Добавление реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateOrUpdateTaskReagentAsync(CreateOrUpdateTaskReagent model)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали  пустую модель");
            }

            if (model.TakenQuantity < model.ReturnedQuantity)
            {
                return new BaseApiResponse(false, "Вы не можете вернуть больше чем взяли");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            var taskRepo = GetRepository<ChemistryTask>();

            var chemistryTask = await taskRepo.Query().FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if(chemistryTask == null)
            {
                return new BaseApiResponse(false, "Химическая задача не найдена по указанному идентификатору");
            }

            var userId = UserId;

            if(chemistryTask.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем задания");
            }

            if(!await GetRepository<ChemistryReagent>().Query().AnyAsync(x => x.Id == model.ReagentId))
            {
                return new BaseApiResponse(false, "Реагент не найден по указанному идентификатору");
            }

            var taskReagentRepo = GetRepository<ChemistryTaskReagent>();

            var taskReagent = await taskReagentRepo.Query()
                .FirstOrDefaultAsync(x => x.TaskId == model.TaskId && x.ReagentId == model.ReagentId);

            if(taskReagent != null)
            {
                taskReagent.TakenQuantity = model.TakenQuantity;
                taskReagent.ReturnedQuantity = model.ReturnedQuantity;

                taskReagentRepo.UpdateHandled(taskReagent);

                return await TrySaveChangesAndReturnResultAsync("Реагент к химическому заданию отредактирован");
            }
            

            taskReagentRepo.CreateHandled(new ChemistryTaskReagent
            {
                TaskId = model.TaskId,
                ReagentId = model.ReagentId,
                TakenQuantity = model.TakenQuantity,
                ReturnedQuantity = model.ReturnedQuantity,
            });

            return await TrySaveChangesAndReturnResultAsync("Добавлен реагент к химическому заданию");
        }

        /// <summary>
        /// Удалить реагент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveTaskReagentAsync(TaskReagentIdModel model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали  пустую модель");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            var repo = GetRepository<ChemistryTaskReagent>();

            var chemistryTaskReagent = await repo.Query().Include(x => x.Task).FirstOrDefaultAsync(x => x.TaskId == model.TaskId && x.ReagentId == model.ReagentId);

            if (chemistryTaskReagent == null)
            {
                return new BaseApiResponse(false, "Редактируемый реагент не найден по указанным идентификаторам");
            }

            if (chemistryTaskReagent.Task.PerformerUserId != UserId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем задания");
            }

            repo.DeleteHandled(chemistryTaskReagent);

            return await TrySaveChangesAndReturnResultAsync("Реагент к химическому заданию удален");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryReagentsWorker(ICrocoAmbientContext) : base(contextWrapper)
        {
        }
    }
}