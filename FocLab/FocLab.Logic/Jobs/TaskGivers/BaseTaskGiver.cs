using System;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.ContextWrappers;
using Croco.Core.Principal;
using FocLab.Model.Contexts;

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
