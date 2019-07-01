using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FocLab.Logic.Workers.ChemistryTasks
{
    /// <inheritdoc />
    /// <summary>
    /// Класс работающий с химическими заданиями
    /// </summary>
    public class AdminChemistryTasksWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Получить список файлов как методы решений
        /// </summary>
        /// <returns></returns>
        public async Task<List<TempSelectListItem>> GetFileMethodsSelectListAsync()
        {
            var fileMethods = await Context.ChemistryMethodFiles.ToListAsync();

            var fileMethodsSelectList = fileMethods
                .Select(x => new TempSelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();

            fileMethodsSelectList.Add(new TempSelectListItem { Selected = true, Text = "Метод не выбран", Value = 0.ToString() });

            return fileMethodsSelectList;
        }

        /// <summary>
        /// Получить список пользователей переложенный в выпадющий список
        /// </summary>
        /// <returns></returns>
        public async Task<List<TempSelectListItem>> GetUsersSelectListAsync()
        {
            var userId = ContextWrapper.UserId;

            var searcher = new UserSearcher(ApplicationContextWrapper);

            var users = await searcher.SearchUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = users.List.Where(x => x.Rights.All(t => t != UserRight.Admin) && x.Rights.All(t => t != UserRight.SuperAdmin))
                .Select(x => new TempSelectListItem
                {
                    Value = x.Id,
                    Text = $"{x.Name} {x.Email}"
                }).ToList();

            usersSelectList = usersSelectList.Where(x => x.Value != userId).ToList();

            return usersSelectList;
        }

        /// <summary>
        /// Создать химическое задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateTaskAsync(Chemistry_CreateTask model)
        {
            var method = model.FileMethodId != 0 ? await Context.ChemistryMethodFiles.FirstOrDefaultAsync(x => x.Id == model.FileMethodId.ToString()) : null;

            if(method == null)
            {
                return new BaseApiResponse(false, "Не найден метод решения задачи (Обратитесь к разработчикам приложения)");
            }

            var chemistryTask = new ChemistryTask
            {
                Id = Guid.NewGuid().ToString(),
                Deleted = false,
                CreationDate = DateTime.Now,
                DeadLineDate = model.DeadLineDate,
                AdminQuality = model.Quality,
                AdminQuantity = model.Quantity,
                AdminUserId = UserId,
                MethodFileId = method.Id,
                Title = model.Title,
                PerformerUserId = model.PerformerId,
                SubstanceCounterJson = JsonConvert.SerializeObject(Chemistry_SubstanceCounter.GetDefaultCounter())
            };

            Context.ChemistryTasks.Add(chemistryTask);

            return await TrySaveChangesAndReturnResultAsync("Успешно создано");
        }

        /// <summary>
        /// Редактирование химического задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> EditTaskAsync(ChemistryTaskDto model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if(!User.IsAdmin())
            {
                return new BaseApiResponse(false, "У вас недостаточно прав для редактирования задания");
            }

            var searcher = new UserSearcher(ApplicationContextWrapper);


            //нахожу исполнителя
            var user = await searcher.GetUserByIdAsync(model.PerformerUserId);


            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору в поле PerformerId");
            }

            var task = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.Id);
            
            if(task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            Context.Entry(task).State = EntityState.Modified;

            task.MethodFileId = model.MethodFileId;
            task.Title = model.Title;
            task.AdminQuality = model.AdminQuality;
            task.AdminQuantity = model.AdminQuantity;
            task.DeadLineDate = model.DeadLineDate;
            task.PerformerUserId = model.PerformerUserId;

            return await TrySaveChangesAndReturnResultAsync("Задание отредактировано");
        }

        /// <summary>
        /// Удалить химическое задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveTaskAsync(ChemistryTaskDto model)
        {
            if (!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования данной задачи");
            }

            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var task = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if(task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            if(task.Deleted)
            {
                return new BaseApiResponse(false, "Задание уже является удаленным");
            }

            Context.Entry(task).State = EntityState.Modified;
            task.Deleted = true;


            return await TrySaveChangesAndReturnResultAsync("Задание отправлено в удаленные");
        }

        /// <summary>
        /// Отмена удаления химического задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CancelRemoveTaskAsync(ChemistryTaskDto model)
        {
            if (!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования данной задачи");
            }

            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var task = await Context.ChemistryTasks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            if (!task.Deleted)
            {
                return new BaseApiResponse(false, "Задание уже является не удаленным");
            }

            Context.Entry(task).State = EntityState.Modified;
            task.Deleted = false;


            return await TrySaveChangesAndReturnResultAsync("Задание востановлено");
        }

        /// <inheritdoc />
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public AdminChemistryTasksWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}