using FocLab.Logic.Models.Tasks;
using NewFocLab.Logic.Models.Users;
using NewFocLab.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FocLab.Logic.Models.Reagents
{
    public class ChemistryReagentModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ChemistryTaskReagentModel> Tasks { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ChemistryReagent, ChemistryReagentModel>> SelectExpression = t => new ChemistryReagentModel
        {
            Id = t.Id,
            Name = t.Name,
            Tasks = t.Tasks.Select(x => new ChemistryTaskReagentModel
            {
                ReturnedQuantity = x.ReturnedQuantity,
                TakenQuantity = x.TakenQuantity,
                Task = new ChemistryTaskSimpleModel
                {
                    Id = x.Task.Id,
                    Title = x.Task.Title,
                    PerformerUser = new UserModelBase
                    {
                        Id = x.Task.PerformerUser.Id,
                        Email = x.Task.PerformerUser.Email,
                        Name = x.Task.PerformerUser.Name
                    }
                }
            }).ToList()
        };
    }

    public class ChemistryTaskReagentModel
    {
        /// <summary>
        /// Химическое задание
        /// </summary>
        public ChemistryTaskSimpleModel Task { get; set; }

        /// <summary>
        /// Взятое кол-во
        /// </summary>
        public decimal TakenQuantity { get; set; }

        /// <summary>
        /// Колличество которое вернули
        /// </summary>
        public decimal ReturnedQuantity { get; set; }

    }
}
