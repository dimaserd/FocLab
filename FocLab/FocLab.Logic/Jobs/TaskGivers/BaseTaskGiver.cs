using Croco.Core.Data.Abstractions.ContextWrappers;
using Croco.Core.Application;
using FocLab.Model.Contexts;
using Croco.Core.Data.Models.Principal;
using Croco.Core.Data.Models.ContextWrappers;

namespace CrocoShop.Logic.Jobs.TaskGivers
{
    public abstract class BaseTaskGiver
    {
        protected IUserContextWrapper<ChemistryDbContext> GetSystemPrincipalContextWrapper()
        {
            var systemPrincipal = new SystemPrincipal();

            return new UserContextWrapper<ChemistryDbContext>(systemPrincipal, CrocoApp.Application.GetDbContext() as ChemistryDbContext, SystemPrincipal.GetSystemId);
        }

        public abstract void ExecuteTask();
    }
}
