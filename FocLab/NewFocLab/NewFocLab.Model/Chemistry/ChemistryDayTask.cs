using NewFocLab.Model.External;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Химическое задание
    /// </summary>
    public class ChemistryDayTask
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата задания больше день чем просто дата
        /// </summary>
        [Column(TypeName="date")]
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
        public string TaskTargetHtml { get; set; }

        /// <summary>
        /// Отчет Html (Summernote)
        /// </summary>
        public string TaskReviewHtml { get; set; }

        /// <summary>
        /// Комментарий Html (Summernote)
        /// </summary>
        public string TaskCommentHtml { get; set; }

        #region Свойства отношений

        /// <summary>
        /// Идентификатор администратора
        /// </summary>
        [ForeignKey(nameof(Admin))]
        public string AdminId { get; set; }

        /// <summary>
        /// Администратор
        /// </summary>
        public virtual FocLabUser Admin { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [ForeignKey(nameof(AssigneeUser))]
        public string AssigneeUserId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public virtual FocLabUser AssigneeUser { get; set; }
        #endregion
    }
}