using FocLab.Model.Entities.Chemistry;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ChemistryTaskPerformerFile
    {
        /// <summary>
        /// 
        /// </summary>
        public ChemistryTaskPerformerFile()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public ChemistryTaskPerformerFile(ChemistryTaskDbFile file)
        {
            FileId = file.FileId;
        }

        /// <summary>
        /// 
        /// </summary>
        public int FileId { get; set; }
    }
}