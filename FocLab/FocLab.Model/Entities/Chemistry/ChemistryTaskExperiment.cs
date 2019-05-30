using FocLab.Model.Entities.Users.Default;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Сущность описывающая эксперимент к химическому заданию
    /// </summary>
    public class ChemistryTaskExperiment
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Заголовок эксперимента
        /// </summary>
        public string Title { get; set; }

        #region Свойства отношений
        /// <summary>
        /// Идентификатор химического задания
        /// </summary>
        [ForeignKey("ChemistryTask")]
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Химическое задание
        /// </summary>
        public virtual ChemistryTask ChemistryTask { get; set; }

       

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [ForeignKey(nameof(Performer))]
        public string PerformerId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public virtual ApplicationUser Performer { get; set; }
        #endregion

        /// <summary>
        /// Дата заверешения
        /// </summary>
        public DateTime? PerformedDate { get; set; }
        

        /// <summary>
        /// Текст написанный исполнителем
        /// </summary>
        public string PerformerText { get; set; }
        

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// сериализованный объект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJson { get; set; }

        /// <summary>
        /// Файлы принадлежащие к данному заданию
        /// </summary>
        public virtual ICollection<ChemistryTaskExperimentFile> Files { get; set; }
    }
}