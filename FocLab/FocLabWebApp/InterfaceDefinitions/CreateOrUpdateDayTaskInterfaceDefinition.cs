using System.Threading.Tasks;
using Tms.Logic.Models;
using Zoo.GenericUserInterface.Models.Definition;
using Zoo.GenericUserInterface.Services;

namespace FocLab.InterfaceDefinitions
{
    public class CreateOrUpdateDayTaskInterfaceDefinition : UserInterfaceDefinition<CreateOrUpdateDayTask>
    {
        public override Task OverrideInterfaceAsync(GenericUserInterfaceBag bag, GenericUserInterfaceModelBuilder<CreateOrUpdateDayTask> overrider)
        {
            overrider.GetBlockBuilder(x => x.Id)
                .SetHidden();

            return Task.CompletedTask;
        }
    }
}