using FocLab.Model.Entities;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.EntityDtos
{
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