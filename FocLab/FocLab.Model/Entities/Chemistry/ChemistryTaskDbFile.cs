using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FocLab.Model.Enumerations;

namespace FocLab.Model.Entities.Chemistry
{
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
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual DbFile File { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }
}