using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.Models;
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
        public async Task<BaseApiResponse> LoadOrReloadFileForTaskAsync(Chemistry_ChangeFileForTask model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if (model.TaskFile == null)
            {
                return new BaseApiResponse(false, "Вы не подали модель файла");
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

            if (model.File == null)
            {
                return new BaseApiResponse(false, "Файл не подан");
            }

            var existedFile = await Context.ChemistryTaskDbFiles.FirstOrDefaultAsync(x => x.ChemistryTaskId == task.Id && x.Type == model.TaskFile.FileType);

            var fileWorker = new DbFileWorker(ApplicationContextWrapper);

            //загружаю файл
            var fileUploadResult = await fileWorker.UploadUserFileAsync(model.File);

            var file = fileUploadResult.ResponseObject;


            ChemistryTaskDbFile taskFile;
            //Если файла пока не существует
            if (existedFile == null)
            {
                taskFile = new ChemistryTaskDbFile
                {
                    FileId = file.Id,
                    Type = model.TaskFile.FileType,
                    ChemistryTaskId = task.Id
                };

                Context.ChemistryTaskDbFiles.Add(taskFile);

                await Context.SaveChangesAsync();

                return new BaseApiResponse(true, "Файл загружен");
            }

            Context.ChemistryTaskDbFiles.Remove(existedFile);

            taskFile = new ChemistryTaskDbFile
            {
                   
                FileId = file.Id,
                Type = model.TaskFile.FileType,
                ChemistryTaskId = task.Id
            };

            Context.ChemistryTaskDbFiles.Add(taskFile);

            return await TrySaveChangesAndReturnResultAsync("Файл загружен и обновлен");
        }


        /// <summary>
        /// Получить задание по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ChemistryTaskModel> GetChemistryTaskByIdAsync(string id)
        {
            return GetRepository<ChemistryTask>().Query()
                .Select(ChemistryTaskModel.SelectExpression)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        
        /// <summary>
        /// Получить все задания
        /// </summary>
        /// <returns></returns>
        public Task<List<ChemistryTaskModel>> GetAllTasksAsync()
        {
            return GetRepository<ChemistryTask>().Query().Select(ChemistryTaskModel.SelectExpression).ToListAsync();
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
            return GetRepository<ChemistryTask>().Query()
                .Where(x => x.Deleted == false)
                .Select(ChemistryTaskModel.SelectExpression).ToListAsync();
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