using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Дто-модель
    /// </summary>
    public class ChemistryTaskExperimentFile
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        [ForeignKey(nameof(ChemistryTaskExperiment))]
        public string ChemistryTaskExperimentId { get; set; }

        /// <summary>
        /// Эксперимент
        /// </summary>
        public virtual ChemistryTaskExperiment ChemistryTaskExperiment { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [ForeignKey(nameof(File))]
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual DbFile File { get; set; }

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