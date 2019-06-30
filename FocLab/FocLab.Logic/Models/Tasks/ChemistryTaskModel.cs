﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Models.Reagents;
using FocLab.Logic.Models.Tasks;
using FocLab.Model.Entities.Chemistry;
using Newtonsoft.Json;

namespace FocLab.Logic.Models
{
    public class ChemistryTaskModel
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
        /// Администратор задания
        /// </summary>
        public ChemistryTaskUserModelBase AdminUser { get; set; }

        /// <summary>
        /// Исполнитель задания
        /// </summary>
        public ChemistryTaskUserModelBase PerformerUser { get; set; }

        /// <summary>
        /// Метод для решения
        /// </summary>
        public ChemistryMethodFileModel ChemistryMethodFile { get; set; }

        /// <summary>
        /// Файлы
        /// </summary>
        public List<ChemistryTaskFileModel> Files { get; set; }

        /// <summary>
        /// Эксперименты
        /// </summary>
        public List<ChemistryTaskExperimentModel> Experiments { get; set; }

        /// <summary>
        /// Реагенты
        /// </summary>
        public List<ChemistryTaskReagentModel> Reagents { get; set; }
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

        [JsonIgnore]
        internal static Expression<Func<ChemistryTask, ChemistryTaskModel>> SelectExpression = a => new ChemistryTaskModel
        {
            Id = a.Id,
            Files = a.Files.Select(t => new ChemistryTaskFileModel
            {
                FileId = t.FileId,
                Type = t.Type
            }).ToList(),
            AdminQuality = a.AdminQuality,
            AdminQuantity = a.AdminQuantity,
            AdminUser = new ChemistryTaskUserModelBase
            {
                Email = a.AdminUser.Email,
                Name = a.AdminUser.Name,
                UserId = a.AdminUser.Id
            },
            PerformerUser = new ChemistryTaskUserModelBase
            {
                UserId = a.PerformerUser.Id,
                Name = a.PerformerUser.Name,
                Email = a.PerformerUser.Email
            },
            ChemistryMethodFile = a.MethodFileId != null ? new ChemistryMethodFileModel
            {
                Id = a.ChemistryMethodFile.Id,
                FileId = a.ChemistryMethodFile.FileId,
                CreationDate = a.ChemistryMethodFile.CreationDate,
                Name = a.ChemistryMethodFile.Name
            } : null,
            CreationDate = a.CreationDate,
            DeadLineDate = a.DeadLineDate,
            Deleted = a.Deleted,
            PerformedDate = a.PerformedDate,
            PerformerQuality = a.PerformerQuality,
            PerformerQuantity = a.PerformerQuantity,
            PerformerText = a.PerformerText,
            SubstanceCounterJson = a.SubstanceCounterJson,
            Title = a.Title,
            Experiments = a.Experiments.Select(x => new ChemistryTaskExperimentModel
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
                    UserId = x.Performer.Id,
                    Email = x.Performer.Email,
                    Name = x.Performer.Name
                },
                PerformerText = x.PerformerText,
                PerformerId = x.PerformerId,
                SubstanceCounterJson = x.SubstanceCounterJson,
                Title = x.Title
            }).ToList(),
            Reagents = a.Reagents.Select(x => new ChemistryTaskReagentModel
            {
                ReturnedQuantity = x.ReturnedQuantity,
                TakenQuantity = x.TakenQuantity,
                Reagent = new ChemistryReagentNameAndIdModel
                {
                    Id = x.Reagent.Id,
                    Name = x.Reagent.Name
                }
            }).ToList()
            
        };

    }
}