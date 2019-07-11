using FocLab.Logic.Models.Users.Projection;
using FocLab.Model.Entities.Tasker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tms.Logic.Models.Tasker
{
    public class DayTaskModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Дата задания больше день чем просто дата
        /// </summary>
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// Текст задания
        /// </summary>
        public string TaskText { get; set; }

        /// <summary>
        /// Название задания
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// Цель Html (Summernote)
        /// </summary>
        public string TaskTarget { get; set; }

        /// <summary>
        /// Отчет Html (Summernote)
        /// </summary>
        public string TaskReview { get; set; }

        /// <summary>
        /// Комментарий Html (Summernote)
        /// </summary>
        public string TaskComment { get; set; }

        public List<DayTaskCommentModel> Comments { get; set; }
        #region Свойства отношений


        /// <summary>
        /// Администратор
        /// </summary>
        public UserFullNameEmailAndAvatarModel Author { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserFullNameEmailAndAvatarModel AssigneeUser { get; set; }
        #endregion

        [JsonIgnore]
        internal static Expression<Func<ApplicationDayTask, DayTaskModel>> SelectExpression = x => new DayTaskModel
        {
            Id = x.Id,
            TaskTitle = x.TaskTitle,
            TaskReview = x.TaskReview,
            TaskText = x.TaskText,
            FinishDate = x.FinishDate,
            TaskDate = x.TaskDate,
            TaskComment = x.TaskComment,
            TaskTarget = x.TaskTarget,

            Author = new UserFullNameEmailAndAvatarModel
            {
                Id = x.Author.Id,
                Name = x.Author.Name,
                Email = x.Author.Email,
                AvatarFileId = x.Author.AvatarFileId,
                Patronymic = x.Author.Patronymic,
                Surname = x.Author.Surname
            },
            AssigneeUser = new UserFullNameEmailAndAvatarModel
            {
                Id = x.AssigneeUser.Id,
                Name = x.AssigneeUser.Name,
                Email = x.AssigneeUser.Email,
                AvatarFileId = x.AssigneeUser.AvatarFileId,
                Patronymic = x.AssigneeUser.Patronymic,
                Surname = x.AssigneeUser.Surname
            },
            Comments = x.Comments.Select(t => new DayTaskCommentModel
            {
                Id = t.Id,
                Comment = t.Comment,
                Author = new UserFullNameEmailAndAvatarModel
                {
                    Id = t.Author.Id,
                    Name = t.Author.Name,
                    Email = t.Author.Email,
                    AvatarFileId = t.Author.AvatarFileId,
                    Patronymic = t.Author.Patronymic,
                    Surname = t.Author.Surname
                }
            }).ToList()
        };
    }
}
