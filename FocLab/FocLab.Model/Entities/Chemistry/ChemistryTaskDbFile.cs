using System.ComponentModel.DataAnnotations.Schema;
using Croco.Core.Model.Models;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FocLab.Model.Entities.Chemistry
{
    /// <summary>
    /// Файл для химического задания
    /// </summary>
    public class ChemistryTaskDbFile : AuditableEntityBase
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [ForeignKey(nameof(ChemistryTask))]
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        [JsonIgnore]
        public virtual ChemistryTask ChemistryTask { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [ForeignKey(nameof(File))]
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        [JsonIgnore]
        public virtual DbFile File { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChemistryTaskDbFile>()
                .HasKey(p => new { p.ChemistryTaskId, p.FileId });
        }

        public string GetComposedId()
        {
            return $"ChemistryTaskId={ChemistryTaskId}&FileId={FileId}";
        }
    }
}