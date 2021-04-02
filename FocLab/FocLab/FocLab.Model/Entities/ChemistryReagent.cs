using Croco.Core.Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FocLab.Model.Entities
{
    /// <summary>
    /// Реагент химической задачи которые нужно использовать для задания
    /// </summary>
    public class ChemistryReagent : AuditableEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Задания
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ChemistryTaskReagent> Tasks { get; set; }
    }
}