using System.Threading.Tasks;
using FocLab.Logic.Models.Users;
using Zoo.GenericUserInterface.Models.Definition;
using Zoo.GenericUserInterface.Services;

namespace FocLab.InterfaceDefinitions
{
    public class CreateUserModelUserInterfaceDefinition : UserInterfaceDefinition<CreateUserModel>
    {
        public override Task OverrideInterfaceAsync(GenericUserInterfaceBag bag, GenericUserInterfaceModelBuilder<CreateUserModel> overrider)
        {
            overrider.GetBlockBuilder(x => x.ConfirmPassword)
                .SetHidden();

            overrider.GetBlockBuilder(x => x.Rights)
                .SetHidden();

            return Task.CompletedTask;
        }
    }
}