using FocLab.Logic.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Experiments
{
    public class ChemistryTaskExperimentSimpleModel
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Заголовок эксперимента
        /// </summary>
        [Display(Name = "Название")]
        public string Title { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserModelBase Performer { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }
    }
}