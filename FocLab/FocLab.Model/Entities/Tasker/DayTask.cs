using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Croco.Core.Model.Abstractions.Auditable;
using Croco.Core.Model.Abstractions.Entity;
using Croco.Core.Model.Models;
using FocLab.Model.Entities.Users.Default;
using Newtonsoft.Json;

namespace FocLab.Model.Entities.Tasker
{
    /// <summary>
    /// Химическое задание
    /// </summary>
    public class ApplicationDayTask : DayTask<ApplicationUser, ApplicationDayTaskComment>
    {

    }

    public class DayTask<TUser, TDayTaskComment> : AuditableEntityBase, IAuditableStringId where TUser : class, ICrocoUser where TDayTaskComment : class
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Дата задания больше день чем просто дата
        /// </summary>
        [Column(TypeName = "date")]
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

        /// <summary>
        /// Оценка исполнителя
        /// </summary>
        public int EstimationSeconds { get; set; }

        /// <summary>
        /// Фактически затраченное время
        /// </summary>
        public int CompletionSeconds { get; set; }

        public int Seconds { get; set; }

        #region Свойства отношений

        /// <summary>
        /// Идентификатор автора данного задания
        /// </summary>
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        /// <summary>
        /// Администратор
        /// </summary>
        [JsonIgnore]
        public virtual TUser Author { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [ForeignKey(nameof(AssigneeUser))]
        public string AssigneeUserId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [JsonIgnore]
        public virtual TUser AssigneeUser { get; set; }

        /// <summary>
        /// Комментарии к данному заданию
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<TDayTaskComment> Comments { get; set; }
        #endregion
    }
}
