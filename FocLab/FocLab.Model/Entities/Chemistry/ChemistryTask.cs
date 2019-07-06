using Croco.Core.Model.Interfaces.Auditable;
using Croco.Core.Model.Models;
using FocLab.Model.Entities.Users.Default;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Химическое задание
    /// </summary>
    public class ChemistryTask : AuditableEntityBase, IAuditableStringId
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
        [ForeignKey(nameof(AdminUser))]
        public string AdminUserId { get; set; }

        /// <summary>
        /// Администратор задания
        /// </summary>
        [JsonIgnore]
        public virtual ApplicationUser AdminUser { get; set; }


        /// <summary>
        /// Исполнитель кому назначено задание
        /// </summary>
        [ForeignKey(nameof(PerformerUser))]
        public string PerformerUserId { get; set; }

        /// <summary>
        /// Исполнитель задания
        /// </summary>
        [JsonIgnore]
        public virtual ApplicationUser PerformerUser { get; set; }


        /// <summary>
        /// Ссылка на метод решения данной задачи (ссылка на файл в системе)
        /// </summary>
        [ForeignKey(nameof(ChemistryMethodFile))]
        public string MethodFileId { get; set; }

        /// <summary>
        /// Метод для решения
        /// </summary>
        [JsonIgnore]
        public virtual ChemistryMethodFile ChemistryMethodFile { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ChemistryTaskDbFile> Files { get; set; }

        /// <summary>
        /// Эксперименты
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ChemistryTaskExperiment> Experiments { get; set; }

        /// <summary>
        /// Реагенты
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ChemistryTaskReagent> Reagents { get; set; }
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
    }
}