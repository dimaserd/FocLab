using System;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Tasks
{
    /// <summary>
    /// Модель для создания химического задания
    /// </summary>
    public class ChemistryCreateTask
    {
        /// <summary>
        /// Идентифкатор администратора
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// Идентификатор Исполнителя
        /// </summary>
        [Display(Name = "Назначить исполнителя")]
        public string PerformerId { get; set; }

        /// <summary>
        /// Условие задачи
        /// </summary>
        [Display(Name = "Условие задачи")]
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [Display(Name = "Метод решения задачи")]
        public string FileMethodId { get; set; }

        /// <summary>
        /// Колличество
        /// </summary>
        [Display(Name = "Колличество")]
        public string Quantity { get; set; }

        /// <summary>
        /// Качество
        /// </summary>
        [Display(Name = "Качество")]
        public string Quality { get; set; }

        /// <summary>
        /// Крайний срок
        /// </summary>
        [Display(Name = "Последний срок выполнения")]
        public DateTime DeadLineDate { get; set; }
    }
}