using System;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Tasks
{
    public class EditChemistryTask
    {
        public string Id { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        [Display(Name = "Условие задачи")]
        public string Title { get; set; }

        [Display(Name = "Качество")]
        public string AdminQuality { get; set; }

        /// <summary>
        /// Дата к которой нужно выполнить задание
        /// </summary>
        [Display(Name = "Последний срок выполнения")]
        public DateTime DeadLineDate { get; set; }

        [Display(Name = "Количество")]
        public string AdminQuantity { get; set; }

        /// <summary>
        /// Дата создания задачи администратором
        /// </summary>
        public DateTime CreationDate { get; set; }

        [Display(Name = "Метод решения задачи")]
        public string MethodFileId { get; set; }

        /// <summary>
        /// Исполнитель кому назначено задание (StringProperty2)
        /// </summary>
        [Display(Name = "Назначить исполнителя")]
        public string PerformerUserId { get; set; }

        public static EditChemistryTask ToEditChemistryTask(ChemistryTaskModel model)
        {
            return new EditChemistryTask
            {
                Id = model.Id,
                AdminQuality = model.AdminQuality,
                AdminQuantity = model.AdminQuantity,
                CreationDate = model.CreationDate,
                DeadLineDate = model.DeadLineDate,
                MethodFileId = model.ChemistryMethodFile?.Id,
                PerformerUserId = model.PerformerUser.Id,
                Title = model.Title
            };
        }
    }
}