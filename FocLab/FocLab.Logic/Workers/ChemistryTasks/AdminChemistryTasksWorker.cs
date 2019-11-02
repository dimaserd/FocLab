using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Entities.Chemistry;
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
        public Task<List<FileMethodModel>> GetFileMethodsAsync()
        {
            return GetRepository<ChemistryMethodFile>().Query()
                .Select(FileMethodModel.SelectExpression).ToListAsync();
        }

        /// <summary>
        /// Создать химическое задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateTaskAsync(ChemistryCreateTask model)
        {
            var isNull = string.IsNullOrEmpty(model.FileMethodId);

            var method = !isNull ? await Context.ChemistryMethodFiles.FirstOrDefaultAsync(x => x.Id == model.FileMethodId.ToString()) : null;

            if(method == null && !isNull)
            {
                return new BaseApiResponse(false, "Не найден выбранный метод решения задачи");
            }

            var repo = GetRepository<ChemistryTask>();

            var chemistryTask = new ChemistryTask
            {
                Id = Guid.NewGuid().ToString(),
                Deleted = false,
                CreationDate = DateTime.Now,
                DeadLineDate = model.DeadLineDate,
                AdminQuality = model.Quality,
                AdminQuantity = model.Quantity,
                AdminUserId = UserId,
                MethodFileId = method?.Id,
                Title = model.Title,
                PerformerUserId = model.PerformerId,
                SubstanceCounterJson = JsonConvert.SerializeObject(Chemistry_SubstanceCounter.GetDefaultCounter())
            };

            repo.CreateHandled(chemistryTask);

            return await TrySaveChangesAndReturnResultAsync("Успешно создано");
        }

        /// <summary>
        /// Редактирование химического задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> EditTaskAsync(EditChemistryTask model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if(!User.IsAdmin())
            {
                return new BaseApiResponse(false, "У вас недостаточно прав для редактирования задания");
            }

            var searcher = new UserSearcher(AmbientContext);

            //нахожу исполнителя
            var user = await searcher.GetUserByIdAsync(model.PerformerUserId);

            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору в поле PerformerId");
            }

            var repo = GetRepository<ChemistryTask>();

            var task = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);
            
            if(task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            task.MethodFileId = model.MethodFileId;
            task.Title = model.Title;
            task.AdminQuality = model.AdminQuality;
            task.AdminQuantity = model.AdminQuantity;
            task.DeadLineDate = model.DeadLineDate;
            task.PerformerUserId = model.PerformerUserId;

            repo.UpdateHandled(task);

            return await TrySaveChangesAndReturnResultAsync("Задание отредактировано");
        }

        /// <summary>
        /// Удалить химическое задание
        /// </summary>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveTaskAsync(string id)
        {
            if (!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования данной задачи");
            }
            var repo = GetRepository<ChemistryTask>();

            var task = await repo.Query().FirstOrDefaultAsync(x => x.Id == id);

            if(task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            if(task.Deleted)
            {
                return new BaseApiResponse(false, "Задание уже является удаленным");
            }

            task.Deleted = true;

            repo.UpdateHandled(task);

            return await TrySaveChangesAndReturnResultAsync("Задание отправлено в удаленные");
        }

        /// <summary>
        /// Отмена удаления химического задания
        /// </summary>
        /// <returns></returns>
        public async Task<BaseApiResponse> CancelRemoveTaskAsync(string id)
        {
            if (!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования данной задачи");
            }

            var repo = GetRepository<ChemistryTask>();

            var task = await repo.Query().FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            if (!task.Deleted)
            {
                return new BaseApiResponse(false, "Задание уже является не удаленным");
            }

            task.Deleted = false;
            repo.UpdateHandled(task);

            return await TrySaveChangesAndReturnResultAsync("Задание востановлено");
        }

        /// <inheritdoc />
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public AdminChemistryTasksWorker(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}