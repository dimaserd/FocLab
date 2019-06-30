using MigrationTool.OldDbContext.Custom;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PencilNCo.Mvc.Areas.Chemistry.Entities
{
    /// <summary>
    /// Метод решения (Является файлом)
    /// </summary>
    public class ChemistryMethodFile
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
        /// Идентификатор файла
        /// </summary>
        [ForeignKey(nameof(File))]
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual CustomDbFile File { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }

    /// <summary>
    /// Метод решения (Является файлом)
    /// </summary>
    public class ChemistryMethodFileDto
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
        /// Идентификатор файла
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual CustomDbFileDto File { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}