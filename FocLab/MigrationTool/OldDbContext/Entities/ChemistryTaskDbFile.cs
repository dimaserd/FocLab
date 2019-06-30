using MigrationTool.OldDbContext.Custom;
using PencilNCo.Model.Entities.Custom;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PencilNCo.Mvc.Areas.Chemistry.Entities
{
    /// <summary>
    /// Тип файла для химического задания
    /// </summary>
    public enum ChemistryTaskDbFileType
    {
        /// <summary>
        /// Изображение реакции
        /// </summary>
        ReactionSchemaImage,

        /// <summary>
        /// Файл 1
        /// </summary>
        File1,

        /// <summary>
        /// Файл 2
        /// </summary>
        File2, 

        /// <summary>
        /// Файл 3
        /// </summary>
        File3,

        /// <summary>
        /// Файл 4
        /// </summary>
        File4,
    }

    /// <summary>
    /// Файл для химического задания
    /// </summary>
    public class ChemistryTaskDbFile
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(ChemistryTask))]
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        public virtual ChemistryTask ChemistryTask { get; set; }

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
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }

    /// <summary>
    /// Файл для химического задания
    /// </summary>
    public class ChemistryTaskDbFileDto : CustomEntityStringKey
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        public ChemistryTaskDto ChemistryTask { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public CustomDbFileDto File { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }


    }
}