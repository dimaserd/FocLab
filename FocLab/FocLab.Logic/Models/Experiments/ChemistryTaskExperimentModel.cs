using FocLab.Model.Entities.Chemistry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FocLab.Logic.Models.Experiments
{
    public class ChemistryTaskExperimentModel
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
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public ChemistryTaskUserModelBase Performer { get; set; }
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
        public List<ChemistryTaskExperimentFileModel> Files { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ChemistryTaskExperiment, ChemistryTaskExperimentModel>> SelectExpression = x => new ChemistryTaskExperimentModel
        {
            Id = x.Id,
            ChemistryTaskId = x.ChemistryTaskId,
            CreationDate = x.CreationDate,
            Deleted = x.Deleted,
            Files = x.Files.Select(t => new ChemistryTaskExperimentFileModel
            {
                FileId = t.FileId,
                Type = t.Type
            }).ToList(),
            PerformedDate = x.PerformedDate,
            Performer = new ChemistryTaskUserModelBase
            {
                Id = x.Performer.Id,
                Email = x.Performer.Email,
                Name = x.Performer.Name
            },
            PerformerText = x.PerformerText,
            SubstanceCounterJson = x.SubstanceCounterJson,
            Title = x.Title
        };
    }
}