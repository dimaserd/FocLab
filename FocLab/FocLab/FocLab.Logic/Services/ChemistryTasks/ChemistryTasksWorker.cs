using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Users;
using FocLab.Model.Entities;
using FocLab.Model.External;

namespace FocLab.Logic.Services.ChemistryTasks
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для работы с химическими заданиями
    /// </summary>
    public class ChemistryTasksWorker : FocLabService
    {
        /// <inheritdoc />
        /// <summary>
        /// Конструктор
        /// </summary>
        public ChemistryTasksWorker(ICrocoAmbientContextAccessor context,
            ICrocoApplication application)
            : base(context, application)
        {
        }

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

            if (task.PerformerUserId != userId && !User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования задания. Так как вы не являетесь экспериментатором или администратором.");
            }

            if (task.Deleted)
            {
                return new BaseApiResponse(false, "Задание является удаленным");
            }

            var fileRepo = GetRepository<ChemistryTaskDbFile>();

            var existedFile = await fileRepo.Query()
                .FirstOrDefaultAsync(x => x.ChemistryTaskId == task.Id && x.Type == model.FileType);

            if (existedFile != null)
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

        private IQueryable<ChemistryTaskModel> GetNoUsersInitQuery()
        {
            return GetRepository<ChemistryTask>().Query()
                .Select(ChemistryTaskModel.NoUsersSelectExpression);
        }

        private static List<ChemistryTaskModel> JoinUsersInMemory(List<ChemistryTaskModel> tasks, List<UserModelBase> users)
        {
            var tasksWithAdmins = from t in tasks
                                  join u in users on t.AdminUser.Id equals u.Id
                                  select new { Task = t, Admin = u };

            var tasksWithPerformers = from t in tasks
                                      join u in users on t.PerformerUser.Id equals u.Id
                                      select new { Task = t, Performer = u };


            foreach (var task in tasksWithAdmins)
            {
                task.Task.AdminUser = new UserModelBase
                {
                    Id = task.Admin.Id,
                    Email = task.Admin.Email,
                    Name = task.Admin.Name
                };
            }

            foreach (var task in tasksWithPerformers)
            {
                task.Task.PerformerUser = new UserModelBase
                {
                    Id = task.Performer.Id,
                    Name = task.Performer.Name,
                    Email = task.Performer.Email
                };
            }

            return tasks;
        }

        private static ChemistryTaskModel JoinUsersInMemory(ChemistryTaskModel task, List<UserModelBase> users)
        {
            var admin = users.First(x => x.Id == task.AdminUser.Id);

            var performer = users.First(x => x.Id == task.PerformerUser.Id);

            task.AdminUser = new UserModelBase
            {
                Id = admin.Id,
                Email = admin.Email,
                Name = admin.Name
            };

            task.PerformerUser = new UserModelBase
            {
                Id = performer.Id,
                Name = performer.Name,
                Email = performer.Email
            };

            return task;
        }

        /// <summary>
        /// Получить задание по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ChemistryTaskModel> GetChemistryTaskByIdAsync(string id)
        {
            var task = await GetNoUsersInitQuery().FirstOrDefaultAsync(x => x.Id == id);

            return JoinUsersInMemory(task, await GetUsers());
        }


        /// <summary>
        /// Получить все задания
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChemistryTaskModel>> GetAllTasksAsync(List<UserModelBase> users)
        {
            var tasks = await GetNoUsersInitQuery()
                .ToListAsync();

            return JoinUsersInMemory(tasks, users);
        }

        /// <summary>
        /// Получить все задания
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChemistryTaskModel>> GetAllTasksAsync()
        {

            var tasks = await GetNoUsersInitQuery()
                .ToListAsync();

            return JoinUsersInMemory(tasks, await GetUsers());
        }

        private Task<List<UserModelBase>> GetUsers()
        {
            return Query<FocLabUser>().Select(x => new UserModelBase
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).ToListAsync();
        }

        /// <summary>
        /// Получить не удаленные задания
        /// </summary>
        /// <param>
        ///     <name>myDb</name>
        /// </param>
        /// <returns></returns>
        public async Task<List<ChemistryTaskModel>> GetNotDeletedTasksAsync()
        {
            var tasks = await GetNoUsersInitQuery()
                .Where(x => x.Deleted == false)
                .ToListAsync();

            return JoinUsersInMemory(tasks, await GetUsers());
        }
    }
}