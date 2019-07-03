using System;
using FocLab.Logic.Models.Tasks;

namespace FocLab.Logic.Models.ChemistryTasks
{
    public class EditChemistryTask
    {
        public string Id { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        public string Title { get; set; }

        public string AdminQuality { get; set; }

        /// <summary>
        /// Дата к которой нужно выполнить задание
        /// </summary>
        public DateTime DeadLineDate { get; set; }


        public string AdminQuantity { get; set; }

        /// <summary>
        /// Дата создания задачи администратором
        /// </summary>
        public DateTime CreationDate { get; set; }

        public string MethodFileId { get; set; }

        /// <summary>
        /// Исполнитель кому назначено задание (StringProperty2)
        /// </summary>
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
                MethodFileId = model.ChemistryMethodFile.Id,
                PerformerUserId = model.PerformerUser.UserId,
                Title = model.Title
            };
        }
    }
}