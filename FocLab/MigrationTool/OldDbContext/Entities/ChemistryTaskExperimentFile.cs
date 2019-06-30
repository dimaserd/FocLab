using MigrationTool.OldDbContext.Custom;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PencilNCo.Mvc.Areas.Chemistry.Entities
{
    /// <summary>
    /// Файл эксперимента для химического задания
    /// </summary>
    public class ChemistryTaskExperimentFile
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(ChemistryTaskExperiment))]
        public string ChemistryTaskExperimentId { get; set; }

        /// <summary>
        /// Эксперимент
        /// </summary>
        public virtual ChemistryTaskExperiment ChemistryTaskExperiment { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(File))]
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual CustomDbFile File { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }

    /// <summary>
    /// Дто-модель
    /// </summary>
    public class ChemistryTaskExperimentFileDto
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string ChemistryTaskExperimentId { get; set; }

        /// <summary>
        /// Эксперимент
        /// </summary>
        public virtual ChemistryTaskExperimentDto ChemistryTaskExperiment { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual CustomDbFileDto File { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }
}