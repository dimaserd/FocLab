using Croco.Core.Abstractions;
using FocLab.Logic.Models.Experiments;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// Изменение файла для химического задания
    /// </summary>
    public class ChemistryChangeFileForTask
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string TaskId { get; set; }
        
        /// <summary>
        /// Идентификатор нового файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Тип файла для химического задания
        /// </summary>
        public ChemistryTaskDbFileType FileType { get; set; }
    }
}