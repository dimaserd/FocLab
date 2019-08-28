using Croco.Core.Abstractions;
using Croco.Core.Application;
using FocLab.Model.Contexts;
using Croco.Core.Data.Models.Principal;
using Croco.Core.Data.Models.ContextWrappers;
using Zoo;
using FocLab.Logic.Implementations;

namespace FocLab.Logic.Jobs.TaskGivers
{
    public abstract class BaseTaskGiver
    {
        protected IUserRequestWithRepositoryFactory GetSystemPrincipalContextWrapper()
        {
            var systemPrincipal = new SystemPrincipal();

            return new UserContextWrapper<ChemistryDbContext>(new SystemCrocoPrincipal(systemPrincipal), CrocoApp.Application.As<FocLabWebApplication>().GetDbContext() as ChemistryDbContext);
        }

        public abstract void ExecuteTask();
    }
}
