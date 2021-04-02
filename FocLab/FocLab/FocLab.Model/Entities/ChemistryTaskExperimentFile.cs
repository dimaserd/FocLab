using Croco.Core.Model.Models;
using Microsoft.EntityFrameworkCore;
using FocLab.Model.Enumerations;
using FocLab.Model.External;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities
{
    /// <summary>
    /// Дто-модель
    /// </summary>
    public class ChemistryTaskExperimentFile : AuditableEntityBase
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        [ForeignKey(nameof(ChemistryTaskExperiment))]
        public string ChemistryTaskExperimentId { get; set; }

        /// <summary>
        /// Эксперимент
        /// </summary>
        [JsonIgnore]
        public virtual ChemistryTaskExperiment ChemistryTaskExperiment { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [ForeignKey(nameof(File))]
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual FocLabDbFile File { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChemistryTaskExperimentFile>()
                .HasKey(p => new { p.ChemistryTaskExperimentId, p.FileId });
        }
    }
}