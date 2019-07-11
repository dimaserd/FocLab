using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Models.Tasks;

namespace FocLab.ViewModels
{
    public class RenderReactionMainSchemaImageViewModel
    {
        public ChemistryTaskFileModel ReactionSchemaImage { get; set; }

        public static RenderReactionMainSchemaImageViewModel Create(ChemistryTaskModel model)
        {
            return new RenderReactionMainSchemaImageViewModel
            {
                ReactionSchemaImage = model.ReactionSchemaImage
            };
        }

        public static RenderReactionMainSchemaImageViewModel Create(ChemistryTaskExperimentModel model)
        {
            return new RenderReactionMainSchemaImageViewModel
            {
                ReactionSchemaImage = model.ReactionSchemaImage != null ? new ChemistryTaskFileModel
                {
                    FileId = model.ReactionSchemaImage.FileId,
                    Type = model.ReactionSchemaImage.Type
                } : null
            };
        }
    }
}
