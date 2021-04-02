using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models
{

    /// <summary>
    /// Изменить файл для эксперимента
    /// </summary>
    public class ChemistryChangeFileForExperiment
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string ExperimentId { get; set; }

        /// <summary>
        /// Тип файла для эксперимента
        /// </summary>
        public ChemistryTaskDbFileType FileType { get; set; }
        
        /// <summary>
        /// Идентифкатор нового файла
        /// </summary>
        public int FileId { get;set;}
    }
}