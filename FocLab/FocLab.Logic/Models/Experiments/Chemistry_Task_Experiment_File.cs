using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// Файл для эксперимента 
    /// </summary>
    public class Chemistry_Task_Experiment_File
    {
        /// <summary>
        /// 
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskDbFileType FileType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}