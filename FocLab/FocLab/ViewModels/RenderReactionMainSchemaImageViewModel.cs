using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Models.Tasks;

namespace FocLab.ViewModels
{
    public class RenderReactionMainSchemaImageViewModel
    {
        public ChemistryTaskFileModel ReactionSchemaImage { get; set; }

        public bool CanEdit { get; private set; }

        public static RenderReactionMainSchemaImageViewModel Create(ChemistryTaskModel model, bool isAdmin, string userId)
        {
            return new RenderReactionMainSchemaImageViewModel
            {
                ReactionSchemaImage = model.ReactionSchemaImage,
                CanEdit = isAdmin || model.PerformerUser.Id == userId
            };
        }

        public static RenderReactionMainSchemaImageViewModel Create(ChemistryTaskExperimentModel model, bool isAdmin, string userId)
        {
            return new RenderReactionMainSchemaImageViewModel
            {
                ReactionSchemaImage = model.ReactionSchemaImage != null ? new ChemistryTaskFileModel
                {
                    FileId = model.ReactionSchemaImage.FileId,
                    Type = model.ReactionSchemaImage.Type
                } : null,
                CanEdit = isAdmin || model.Performer.Id == userId
            };
        }
    }
}