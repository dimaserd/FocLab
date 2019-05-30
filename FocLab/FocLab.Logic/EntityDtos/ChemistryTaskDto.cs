using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Model.Entities.Chemistry;

namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// Dto модель
    /// </summary>
    public class ChemistryTaskDto
    {
        public string Id { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Дата к которой нужно выполнить задание
        /// </summary>
        public DateTime DeadLineDate { get; set; }

        /// <summary>
        /// Дата выполнения задачи исполнителем
        /// </summary>
        public DateTime? PerformedDate { get; set; }

        /// <summary>
        /// Дата создания задачи администратором
        /// </summary>
        public DateTime CreationDate { get; set; }


        #region Свойства отношений
        /// <summary>
        /// Тот кто дал задание (StringProperty1)
        /// </summary>
        public string AdminUserId { get; set; }

        /// <summary>
        /// Администратор задания
        /// </summary>
        public ApplicationUserDto AdminUser { get; set; }


        /// <summary>
        /// Исполнитель кому назначено задание (StringProperty2)
        /// </summary>
        public string PerformerUserId { get; set; }

        /// <summary>
        /// Исполнитель задания
        /// </summary>
        public ApplicationUserDto PerformerUser { get; set; }


        /// <summary>
        /// Ссылка на метод решения данной задачи (ссылка на файл в системе)
        /// </summary>
        public string MethodFileId { get; set; }

        /// <summary>
        /// Метод решения
        /// </summary>
        public ChemistryMethodFileDto ChemistryMethodFile { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public List<ChemistryTaskDbFileDto> Files { get; set; }

        /// <summary>
        /// Эксперименты
        /// </summary>
        public List<ChemistryTaskExperimentDto> Experiments { get; set; }

        /// <summary>
        /// Реагенты
        /// </summary>
        public List<ChemistryTaskReagentDto> Reagents { get; set; }
        #endregion
        /// <summary>
        /// Колличество установленное администратором
        /// </summary>
        public string AdminQuantity { get; set; }

        /// <summary>
        /// Качество установленное администратором
        /// </summary>
        public string AdminQuality { get; set; }

        /// <summary>
        /// Колличество установленное исполнителем
        /// </summary>
        public string PerformerQuantity { get; set; }

        /// <summary>
        /// Качество установленное исполнителем
        /// </summary>
        public string PerformerQuality { get; set; }

        /// <summary>
        /// Текст установленный исполнителем
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// сериализованный обьект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJson { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Функция выборки
        /// </summary>
        public static Expression<Func<ChemistryTask, ChemistryTaskDto>> SelectExpression = x => new ChemistryTaskDto
        {
            Id = x.Id,
            Deleted = x.Deleted,
            DeadLineDate = x.DeadLineDate,
            AdminUserId = x.AdminUserId,
            Title = x.Title,
            AdminQuality = x.AdminQuantity,
            AdminQuantity = x.AdminQuantity,
            MethodFileId = x.MethodFileId,
            PerformerUserId = x.PerformerUserId,
            SubstanceCounterJson = x.SubstanceCounterJson,
            CreationDate = x.CreationDate
        };
    }
}