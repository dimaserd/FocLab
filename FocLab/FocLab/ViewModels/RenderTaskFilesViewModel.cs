using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Models.Tasks;
using FocLab.Model.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace FocLab.ViewModels
{
    public class RenderTaskFilesViewModel
    {
        static readonly List<ChemistryTaskDbFileType> ForeachTypes = new List<ChemistryTaskDbFileType>
        {
            ChemistryTaskDbFileType.File1,
            ChemistryTaskDbFileType.File2,
            ChemistryTaskDbFileType.File3,
            ChemistryTaskDbFileType.File4
        };

        public List<ChemistryTaskDbFileType> Types { get; set; }

        public List<ChemistryTaskFileModel> Files { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsPerformer { get; set; }

        public bool CanEdit => IsAdmin || IsPerformer;

        public static RenderTaskFilesViewModel Create(ChemistryTaskModel task, bool isAdmin, string userId)
        {
            return new RenderTaskFilesViewModel
            {
                Types = ForeachTypes,
                IsAdmin = isAdmin,
                IsPerformer = task.PerformerUser.Id == userId,
                Files = task.Files
            };
        }

        public static RenderTaskFilesViewModel Create(ChemistryTaskExperimentModel model, bool isAdmin, string userId)
        {
            return new RenderTaskFilesViewModel
            {
                Types = ForeachTypes,
                IsPerformer = model.Performer.Id == userId,
                Files = model.Files.Select(x => new ChemistryTaskFileModel
                {
                    FileId = x.FileId,
                    Type = x.Type
                }).ToList(),
                IsAdmin = isAdmin
            };
        }

        public static int ToInt(ChemistryTaskDbFileType enumValue)
        {
            return (int)enumValue;
        }
    }
}
