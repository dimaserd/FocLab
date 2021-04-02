using System.Threading.Tasks;
using FocLab.Logic.Models.Experiments;
using Zoo.GenericUserInterface.Models.Definition;
using Zoo.GenericUserInterface.Services;

namespace FocLab.InterfaceDefinitions
{
    public class CreateExperimentInterfaceDefinition : UserInterfaceDefinition<CreateExperiment>
    {
        public override Task OverrideInterfaceAsync(GenericUserInterfaceBag bag, GenericUserInterfaceModelBuilder<CreateExperiment> overrider)
        {
            return Task.CompletedTask;
        }
    }
}