using System;
using System.Collections.Generic;
using FocLab.Logic.EntityDtos.Users.Default;

namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// Dto-модель химического эксперимента
    /// </summary>
    public class ChemistryTaskExperimentDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        #region Свойства отношений
        /// <summary>
        /// Идентификатор химического задания
        /// </summary>
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Химическое задание
        /// </summary>
        public ChemistryTaskDto ChemistryTask { get; set; }



        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string PerformerId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public ApplicationUserDto Performer { get; set; }
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
        /// сериализованный обьект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJson { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public List<ChemistryTaskExperimentFileDto> Files { get; set; }
    }
}