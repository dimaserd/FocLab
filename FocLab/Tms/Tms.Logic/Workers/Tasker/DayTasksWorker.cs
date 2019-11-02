using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Logic.Extensions;
using FocLab.Logic.Resources;
using FocLab.Logic.Workers;
using FocLab.Model.Entities.Tasker;
using FocLab.Model.Entities.Users.Default;
using Microsoft.EntityFrameworkCore;
using Tms.Logic.Models.Tasker;

namespace Tms.Logic.Workers.Tasker
{
    /// <summary>
    /// Класс для работы с заданиями на день 
    /// </summary>
    public class DayTasksWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Задания можно создавать и редактировать за один прошедший день
        /// </summary>
        public int DaysSpan = 1;

        /// <summary>
        /// Получить допустимую дату
        /// </summary>
        /// <returns></returns>
        public DateTime GetAllowedDate()
        {
            var dateNow = DateTime.Now;

            var todayDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);

            return todayDate.AddDays(-DaysSpan);
        }


        /// <summary>
        /// Метод высчитывающий начальную 
        /// </summary>
        /// <param name="dateNow"></param>
        /// <param name="monthShift"></param>
        /// <returns></returns>
        public Tuple<DateTime, DateTime> GetDatesTuple(DateTime dateNow, int monthShift)
        {
            var date = new DateTime(dateNow.Year, dateNow.Month, 1);

            var dateMonthShifted = date.AddMonths(monthShift);

            var firstDayInSearchMonth = new DateTime(dateMonthShifted.Year, dateMonthShifted.Month, 1);

            var lastDayInSearchMonth = new DateTime(dateMonthShifted.Year, dateMonthShifted.Month, DateTime.DaysInMonth(dateMonthShifted.Year, dateMonthShifted.Month));

            return new Tuple<DateTime, DateTime>(firstDayInSearchMonth, lastDayInSearchMonth);
        }

        /// <summary>
        /// Возвращает задания на месяц для текущего пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<DayTaskModel>> GetDayTasksAsync(UserScheduleSearchModel model)
        {
            if (model == null)
            {
                return null;
            }

            var dateNow = DateTime.Now;

            var tuple = GetDatesTuple(dateNow, model.MonthShift);

            var firstDayInSearchMonth = tuple.Item1;

            var lastDayInSearchMonth = tuple.Item2;

            var initQuery = GetRepository<ApplicationDayTask>().Query()
                .Where(x => x.TaskDate >= firstDayInSearchMonth && x.TaskDate <= lastDayInSearchMonth);

            initQuery = model.ShowTasksWithNoAssignee ?
                initQuery.Where(x => x.AssigneeUserId == null)
                : initQuery.Where(x => model.UserIds.Contains(x.AssigneeUserId));

            return initQuery.Select(DayTaskModel.SelectExpression)
                .ToListAsync();
        }

        public Task<DayTaskModel> GetDayTaskByIdAsync(string id)
        {
            return GetRepository<ApplicationDayTask>().Query()
                .Select(DayTaskModel.SelectExpression)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BaseApiResponse> CreateOrUpdateDayTaskAsync(CreateOrUpdateDayTask model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                return CreateDayTaskAsync(model);
            }

            return UpdateDayTaskAsync(model);
        }

        /// <summary>
        /// Создание задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<BaseApiResponse> CreateDayTaskAsync(CreateOrUpdateDayTask model)
        {
            if (model.TaskDate < GetAllowedDate())
            {
                return new BaseApiResponse(false, "Вы не можете создавать задания на прошедшую дату");
            }

            if (string.IsNullOrWhiteSpace(model.TaskTitle))
            {
                return new BaseApiResponse(false, "Пустое название задания");
            }

            if (string.IsNullOrWhiteSpace(model.TaskText))
            {
                return new BaseApiResponse(false, "Пустое описание задания");
            }

            if (!await GetRepository<ApplicationUser>().Query().AnyAsync(x => x.Id == model.AssigneeUserId))
            {
                return new BaseApiResponse(false, "Пользователь не найден по указанному идентификатору");
            }

            var repo = GetRepository<ApplicationDayTask>();

            repo.CreateHandled(new ApplicationDayTask
            {
                AuthorId = UserId,
                AssigneeUserId = model.AssigneeUserId,
                TaskDate = model.TaskDate,
                TaskText = model.TaskText,
                TaskTitle = model.TaskTitle,
                TaskComment = model.TaskComment,
                TaskReview = model.TaskReview,
                TaskTarget = model.TaskTarget
            });

            return await TrySaveChangesAndReturnResultAsync("Задание создано");
        }

        /// <summary>
        /// Редактирование задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<BaseApiResponse> UpdateDayTaskAsync(CreateOrUpdateDayTask model)
        {
            var userId = UserId;

            if (string.IsNullOrWhiteSpace(model.TaskTitle))
            {
                return new BaseApiResponse(false, "Пустое название задания");
            }

            if (string.IsNullOrWhiteSpace(model.TaskText))
            {
                return new BaseApiResponse(false, "Пустое описание задания");
            }

            var repo = GetRepository<ApplicationDayTask>();

            var dayTask = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (dayTask == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            if (dayTask.AssigneeUserId != userId && !User.IsAdmin())
            {
                return new BaseApiResponse(false, "Вы не можете редактировать данное задание, так как вы не являетесь его исполнителем");
            }

            dayTask.TaskTitle = model.TaskTitle;
            dayTask.TaskText = model.TaskText;
            dayTask.TaskDate = model.TaskDate;
            dayTask.TaskComment = model.TaskComment;
            dayTask.TaskReview = model.TaskReview;
            dayTask.TaskTarget = model.TaskTarget;

            repo.UpdateHandled(dayTask);

            return await TrySaveChangesAndReturnResultAsync("Задание обновлено");
        }

        /// <summary>
        /// Добавить комментарий к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<DayTaskModel>> CommentDayTaskAsync(CommentDayTask model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse<DayTaskModel>(false, ValidationMessages.YouAreNotAuthorized);
            }

            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<DayTaskModel>(validation);
            }

            var repo = GetRepository<ApplicationDayTask>();

            var dayTask = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.DayTaskId);

            if (dayTask == null)
            {
                return new BaseApiResponse<DayTaskModel>(false, TaskerResource.DayTaskNotFoundByProvidedId);
            }

            var commentsRepo = GetRepository<ApplicationDayTaskComment>();

            commentsRepo.CreateHandled(new ApplicationDayTaskComment
            {
                AuthorId = UserId,
                Comment = model.Comment,
                DayTaskId = model.DayTaskId
            });

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await SaveChangesAsync();

                var task = await repo.Query().Select(DayTaskModel.SelectExpression).FirstOrDefaultAsync(x => x.Id == model.DayTaskId);

                return new BaseApiResponse<DayTaskModel>(true, TaskerResource.CommentAdded, task);
            });
        }

        public async Task<BaseApiResponse<DayTaskModel>> UpdateDayTaskCommentAsync(UpdateDayTaskComment model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse<DayTaskModel>(false, ValidationMessages.YouAreNotAuthorized);
            }

            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return new BaseApiResponse<DayTaskModel>(validation);
            }

            var commentsRepo = GetRepository<ApplicationDayTaskComment>();

            var comment = await commentsRepo.Query().FirstOrDefaultAsync(x => x.Id == model.DayTaskCommentId);

            if (comment == null)
            {
                return new BaseApiResponse<DayTaskModel>(false, TaskerResource.DayTaskCommentNotFoundById);
            }

            comment.Comment = model.Comment;

            commentsRepo.UpdateHandled(comment);

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await SaveChangesAsync();
                var repo = GetRepository<ApplicationDayTask>();

                var task = await repo.Query()
                    .Select(DayTaskModel.SelectExpression)
                    .FirstOrDefaultAsync(x => x.Id == comment.DayTaskId);

                return new BaseApiResponse<DayTaskModel>(true, TaskerResource.CommentUpdated, task);
            });

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public DayTasksWorker(ICrocoAmbientContext contextWrapper) : base(contextWrapper)
        {
        }
    }
}
