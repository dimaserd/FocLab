using Croco.Core.Utils;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Models.Users;
using FocLab.Model.Entities;
using FocLab.Model.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FocLab.Logic.Models.Experiments
{

    public class ChemistryTaskExperimentModel : ChemistryTaskExperimentSimpleModel
    {
        #region Свойства отношений

        /// <summary>
        /// Задание, к которому был создан эксперимент
        /// </summary>
        public ChemistryTaskSimpleModel Task { get; set; }
        
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
        /// сериализованный объект счетчика веществ 
        /// </summary>
        public string SubstanceCounterJson { get; set; }

        public ChemistryTaskExperimentFileModel ReactionSchemaImage => Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage);


        public Chemistry_SubstanceCounter SubstanceCounter => Tool.JsonConverter.Deserialize<Chemistry_SubstanceCounter>(SubstanceCounterJson ?? "") ?? Chemistry_SubstanceCounter.GetDefaultCounter();

        /// <summary>
        /// Файлы принадлежащие к данному заданию
        /// </summary>
        public List<ChemistryTaskExperimentFileModel> Files { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ChemistryTaskExperiment, ChemistryTaskExperimentModel>> SelectExpression = x => new ChemistryTaskExperimentModel
        {
            Id = x.Id,
            Task = new ChemistryTaskSimpleModel
            {
                Id = x.ChemistryTask.Id,
                Title = x.ChemistryTask.Title,
                PerformerUser = new UserModelBase
                {
                    Id = x.ChemistryTask.PerformerUser.Id,
                    Name = x.ChemistryTask.PerformerUser.Name,
                    Email = x.ChemistryTask.PerformerUser.Email
                }
            },
            CreationDate = x.CreationDate,
            Deleted = x.Deleted,
            Files = x.Files.Select(t => new ChemistryTaskExperimentFileModel
            {
                FileId = t.FileId,
                Type = t.Type
            }).ToList(),
            PerformedDate = x.PerformedDate,
            Performer = new UserModelBase
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