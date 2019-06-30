using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PencilNCo.Mvc.Areas.Chemistry.Entities
{
    /// <summary>
    /// Реагент для химического задания
    /// </summary>
    public class ChemistryTaskReagent
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [Key]
        [ForeignKey(nameof(Task))]
        [Column(Order = 0)]
        public string TaskId { get; set; }

        /// <summary>
        /// Химическое задание
        /// </summary>
        public virtual ChemistryTask Task { get; set; }

        /// <summary>
        /// Идентификатор реагента
        /// </summary>
        [Key]
        [ForeignKey(nameof(Reagent))]
        [Column(Order = 1)]
        public string ReagentId { get; set; }

        /// <summary>
        /// Химический реагент
        /// </summary>
        public virtual ChemistryReagent Reagent { get; set; }

        /// <summary>
        /// Взятое кол-во
        /// </summary>
        public decimal TakenQuantity { get; set; }

        /// <summary>
        /// Колличество которое вернули
        /// </summary>
        public decimal ReturnedQuantity { get; set; }
    }

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