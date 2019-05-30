using System.Collections.Generic;

namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ChemistryReagentDto
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
        public virtual List<ChemistryTaskReagentDto> Tasks { get; set; }
    }
}