using System.Collections.Generic;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Реагент химической задачи которые нужно использовать для задания
    /// </summary>
    public class ChemistryReagent
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
        public virtual ICollection<ChemistryTaskReagent> Tasks { get; set; }
    }
}