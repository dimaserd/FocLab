using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Data.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Tasks;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryTasks
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для работы с химическими заданиями
    /// </summary>
    public class ChemistryTasksWorker : BaseChemistryWorker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> ChangeFileFileForTaskAsync(ChemistryChangeFileForTask model)
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
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования задания. Так как вы не являетесь экспериментатором.");
            }

            if (task.Deleted)
            {
                return new BaseApiResponse(false, "Задание является удаленным");
            }

            var fileRepo = GetRepository<ChemistryTaskDbFile>();

            var existedFile = await fileRepo.Query()
                .FirstOrDefaultAsync(x => x.ChemistryTaskId == task.Id && x.Type == model.FileType);

            if(existedFile != null)
            {
                fileRepo.DeleteHandled(existedFile);
            }

            fileRepo.CreateHandled(new ChemistryTaskDbFile
            {
                FileId = model.FileId,
                Type = model.FileType,
                ChemistryTaskId = model.TaskId
            });

            return await TrySaveChangesAndReturnResultAsync("Файл обновлен");
        }

        private IQueryable<ChemistryTaskModel> GetInitQuery()
        {
            return GetRepository<ChemistryTask>().Query()
                .Select(ChemistryTaskModel.SelectExpression);
        }

        /// <summary>
        /// Получить задание по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ChemistryTaskModel> GetChemistryTaskByIdAsync(string id)
        {
            return GetInitQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        
        /// <summary>
        /// Получить все задания
        /// </summary>
        /// <returns></returns>
        public Task<List<ChemistryTaskModel>> GetAllTasksAsync()
        {
            return GetInitQuery()
                .ToListAsync();
        }

        /// <summary>
        /// Получить не удаленные задания
        /// </summary>
        /// <param>
        ///     <name>myDb</name>
        /// </param>
        /// <returns></returns>
        public Task<List<ChemistryTaskModel>> GetNotDeletedTasksAsync()
        {
            return GetInitQuery()
                .Where(x => x.Deleted == false)
                .ToListAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryTasksWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}