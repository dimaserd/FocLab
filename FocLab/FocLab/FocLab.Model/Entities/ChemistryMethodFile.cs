using Croco.Core.Model.Models;
using FocLab.Model.External;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities
{
    /// <summary>
    /// Метод решения (Является файлом)
    /// </summary>
    public class ChemistryMethodFile : AuditableEntityBase
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
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        [JsonIgnore]
        public virtual FocLabDbFile File { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}