using Croco.Core.Abstractions;
using FocLab.Logic.Models.Experiments;

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
        /// Файл
        /// </summary>
        public IFileData File { get; set; }

        /// <summary>
        /// Если 0 это Изображение реакции, Далее Файл 1, Файл 2, Файл 3, Файл 4, 
        /// </summary>
        public Chemistry_Task_Experiment_File ExperimentFile { get; set; }
    }
}