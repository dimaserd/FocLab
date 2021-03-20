using System.Threading.Tasks;
using FocLab.Logic.Models.Reagents;
using Zoo.GenericUserInterface.Models.Definition;
using Zoo.GenericUserInterface.Services;

namespace FocLab.InterfaceDefinitions
{
    public class ChemistryReagentNameAndIdModelInterfaceDefinition : UserInterfaceDefinition<ChemistryReagentNameAndIdModel>
    {
        public override Task OverrideInterfaceAsync(GenericUserInterfaceBag bag, GenericUserInterfaceModelBuilder<ChemistryReagentNameAndIdModel> overrider)
        {
            overrider.GetBlockBuilder(x => x.Id)
                .SetHidden();
            return Task.CompletedTask;
        }
    }
}