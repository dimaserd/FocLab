using FocLab.Model.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FocLab.Logic.Models.Reagents
{
    public class ChemistryReagentNameAndIdModel
    {
        public string Id { get; set; }

        [Display(Name = "Название реагента")]
        public string Name { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ChemistryReagent, ChemistryReagentNameAndIdModel>> SelectExpression = x => new ChemistryReagentNameAndIdModel
        {
            Id = x.Id,
            Name = x.Name
        };
    }
}