using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models.Experiments
{
    public class ChemistryTaskExperimentFileModel
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }
}