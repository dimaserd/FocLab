﻿using System;
using System.Collections.Generic;

namespace Tms.Logic.Models
{
    public class DayTaskModel
    {
        public DayTaskModel(DayTaskModelWithNoUsersJustIds model, 
            UserFullNameEmailAndAvatarModel author, 
            UserFullNameEmailAndAvatarModel assignee)
        {
            Id = model.Id;
            TaskDate = model.TaskDate;
            TaskText = model.TaskText;
            TaskTitle = model.TaskTitle;
            FinishDate = model.FinishDate;
            TaskTarget = model.TaskTarget;
            TaskReview = model.TaskReview;
            TaskComment = model.TaskComment;
            Author = author;
            AssigneeUser = assignee;
        }

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

        public List<DayTaskCommentModel> Comments { get; set; } = new List<DayTaskCommentModel>();

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
    }
}