using System;
using System.Collections.Generic;

namespace FocLab.Logic.Models.Experiments
{
    /// <summary>
    /// Сущность описывающая эксперимент для химической задачи, получается из мета-сущности по определенному типу
    /// </summary>
    public class Chemistry_Task_Experiment
    {
        /// <summary>
        /// Счетчик веществ
        /// </summary>
        public Chemistry_SubstanceCounter SubstanceCounter { get; set; }

        #region Файлы
        /// <summary>
        /// Изображение реакции
        /// </summary>
        public ChemistryTaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public List<Chemistry_Task_Experiment_File> Files { get; set; }

        #endregion

        /// <summary>
        /// Идентификатор эксперимента (то есть идентификатор мета-сущности)
        /// </summary>
        public string ExperimentId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string PerformerId { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string PerformerEmail { get; set; }

        /// <summary>
        /// Текст написанный исполнителем
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// Идентификатор химического задания
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Название задания к которому назначен эксперимент
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// сериализованный обьект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJSON { get; set; }
    }
}