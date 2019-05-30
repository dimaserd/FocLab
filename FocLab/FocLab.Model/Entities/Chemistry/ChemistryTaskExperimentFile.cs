using FocLab.Model.Enumerations;

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
        public string ChemistryTaskExperimentId { get; set; }

        /// <summary>
        /// Эксперимент
        /// </summary>
        public virtual ChemistryTaskExperiment ChemistryTaskExperiment { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual DbFileDto File { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }
}