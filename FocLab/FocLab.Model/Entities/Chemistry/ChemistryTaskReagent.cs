using Croco.Core.Model.Interfaces.Auditable;
using Croco.Core.Model.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Реагент для химического задания
    /// </summary>
    public class ChemistryTaskReagent : AuditableEntityBase, IAuditableComposedId
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [ForeignKey(nameof(Task))]
        public string TaskId { get; set; }

        /// <summary>
        /// Химическое задание
        /// </summary>
        [JsonIgnore]
        public virtual ChemistryTask Task { get; set; }

        /// <summary>
        /// Идентификатор реагента
        /// </summary>
        [ForeignKey(nameof(Reagent))]
        public string ReagentId { get; set; }

        /// <summary>
        /// Химический реагент
        /// </summary>
        [JsonIgnore]
        public virtual ChemistryReagent Reagent { get; set; }

        /// <summary>
        /// Взятое кол-во
        /// </summary>
        public decimal TakenQuantity { get; set; }

        /// <summary>
        /// Колличество которое вернули
        /// </summary>
        public decimal ReturnedQuantity { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChemistryTaskReagent>()
                .HasKey(p => new { p.TaskId, p.ReagentId });
        }

        public string GetComposedId()
        {
            return $"TaskId={TaskId}&ReagentId={ReagentId}";
        }
    }
}