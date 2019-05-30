﻿namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// Реагент для химического задания
    /// </summary>
    public class ChemistryTaskReagentDto
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Химическое задание
        /// </summary>
        public virtual ChemistryTaskDto Task { get; set; }

        /// <summary>
        /// Идентификатор реагента
        /// </summary>
        public string ReagentId { get; set; }

        /// <summary>
        /// Химический реагент
        /// </summary>
        public virtual ChemistryReagentDto Reagent { get; set; }

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