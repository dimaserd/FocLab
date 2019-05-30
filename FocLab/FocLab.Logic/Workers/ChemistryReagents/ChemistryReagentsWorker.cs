using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.EntityDtos.Users.Default;
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
        public async Task<List<ChemistryReagentDto>> GetReagentsAsync()
        {
            return await Context.ChemistryReagents.Select(x => new ChemistryReagentDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }

        /// <summary>
        /// Получить реагенты
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ChemistryReagentDto> GetReagentAsync(ChemistryReagentDto model)
        {
            return Context.ChemistryReagents.Select(x => new ChemistryReagentDto
            {
                Id = x.Id,
                Name = x.Name,
                Tasks = x.Tasks.Select(t => new ChemistryTaskReagentDto
                {
                    ReagentId = t.ReagentId,
                    Task = new ChemistryTaskDto
                    {
                        Id = t.Task.Id,
                        Title = t.Task.Title,
                        PerformerUser = new ApplicationUserDto
                        {
                            Id = t.Task.PerformerUser.Id,
                            Name = t.Task.PerformerUser.Name,
                            Email = t.Task.PerformerUser.Email
                        }
                    },
                    TakenQuantity = t.TakenQuantity,
                    ReturnedQuantity = t.ReturnedQuantity,
                    TaskId = t.TaskId
                }).ToList(),

            }).FirstOrDefaultAsync(x => x.Id == model.Id);
        }

        /// <summary>
        /// Создание реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateReagentAsync(ChemistryReagentDto model)
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

            if(await Context.ChemistryReagents.AnyAsync(x => x.Name == model.Name))
            {
                return new BaseApiResponse(false, "Реагент с данным именем уже существует");
            }

            Context.ChemistryReagents.Add(new ChemistryReagent
            {
                Id = Guid.NewGuid().ToString(),
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
        public async Task<BaseApiResponse> EditReagentAsync(ChemistryReagentDto model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            model.Name = model.Name.Trim();

            if (await Context.ChemistryReagents.AnyAsync(x => x.Name == model.Name))
            {
                return new BaseApiResponse(false, "Реагент с данным именем уже существует");
            }

            var chemistryReagent = await Context.ChemistryReagents.FirstOrDefaultAsync(x => x.Id == model.Id);

            if(chemistryReagent == null)
            {
                return new BaseApiResponse(false, "Реагент не найден по указанному идентификатору");
            }

            Context.Entry(chemistryReagent).State = EntityState.Modified;

            chemistryReagent.Name = model.Name;

            return await TrySaveChangesAndReturnResultAsync("Реагент отредактирован");
        }


        /// <summary>
        /// Добавление реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> AddTaskReagentAsync(ChemistryTaskReagentDto model)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали  пустую модель");
            }

            if(!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            var chemistryTask = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if(chemistryTask == null)
            {
                return new BaseApiResponse(false, "Химическая задача не найдена по указанному идентификатору");
            }

            var userId = UserId;

            if(chemistryTask.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем задания");
            }

            if(!await Context.ChemistryReagents.AnyAsync(x => x.Id == model.ReagentId))
            {
                return new BaseApiResponse(false, "Реагент не найден по указанному идентификатору");
            }

            if(await Context.ChemistryTaskReagents.AnyAsync(x => x.TaskId == model.TaskId && x.ReagentId == model.ReagentId))
            {
                return new BaseApiResponse(false, "Данный реагент уже добавлен к данному заданию");
            }

            if(model.TakenQuantity < model.ReturnedQuantity)
            {
                return new BaseApiResponse(false, "Вы не можете вернуть больше чем взяли");
            }

            Context.ChemistryTaskReagents.Add(new ChemistryTaskReagent
            {
                TaskId = model.TaskId,
                ReagentId = model.ReagentId,
                TakenQuantity = model.TakenQuantity,
                ReturnedQuantity = model.ReturnedQuantity,
            });

            return await TrySaveChangesAndReturnResultAsync("Добавлен реагент к химическому заданию");
        }

        /// <summary>
        /// Редактирование реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> EditTaskReagentAsync(ChemistryTaskReagentDto model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали  пустую модель");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            var chemistryTaskReagent = await Context.ChemistryTaskReagents.Include(x => x.Task).FirstOrDefaultAsync(x => x.TaskId == model.TaskId && x.ReagentId == model.ReagentId);

            if (chemistryTaskReagent == null)
            {
                return new BaseApiResponse(false, "Редактируемый реагент не найден по указанным идентификаторам");
            }

            var userId = UserId;

            if (chemistryTaskReagent.Task.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем задания");
            }

            if (model.TakenQuantity < model.ReturnedQuantity)
            {
                return new BaseApiResponse(false, "Вы не можете вернуть больше чем взяли");
            }

            Context.Entry(chemistryTaskReagent).State = EntityState.Modified;

            chemistryTaskReagent.TakenQuantity = model.TakenQuantity;
            chemistryTaskReagent.ReturnedQuantity = model.ReturnedQuantity;

            return await TrySaveChangesAndReturnResultAsync("Реагент к химическому заданию отредактирован");
        }

        /// <summary>
        /// Удалить реагент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveTaskReagentAsync(ChemistryTaskReagentDto model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали  пустую модель");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            var chemistryTaskReagent = await Context.ChemistryTaskReagents.Include(x => x.Task).FirstOrDefaultAsync(x => x.TaskId == model.TaskId && x.ReagentId == model.ReagentId);

            if (chemistryTaskReagent == null)
            {
                return new BaseApiResponse(false, "Редактируемый реагент не найден по указанным идентификаторам");
            }

            var userId = UserId;

            if (chemistryTaskReagent.Task.PerformerUserId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем задания");
            }

            Context.ChemistryTaskReagents.Remove(chemistryTaskReagent);

            return await TrySaveChangesAndReturnResultAsync("Реагент к химическому заданию удален");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryReagentsWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}