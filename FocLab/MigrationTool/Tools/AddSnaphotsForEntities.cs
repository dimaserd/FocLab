using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.Implementations;
using Microsoft.EntityFrameworkCore;
using NewFocLab.Model.Entities;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class AddSnaphotsForEntities : FocLabWorker
    {
        public AddSnaphotsForEntities(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
        }

        public async Task<BaseApiResponse> Execute()
        {
            await Update<ChemistryMethodFile>();
            await Update<ChemistryReagent>();
            await Update<ChemistryTask>();
            await Update<ChemistryTaskDbFile>();
            await Update<ChemistryTaskExperiment>();
            await Update<ChemistryTaskExperimentFile>();
            await Update<ChemistryTaskReagent>();

            return await TrySaveChangesAndReturnResultAsync("Завершено");
        }

        private async Task Update<T>() where T : class
        {
            var repo = GetRepository<T>();

            var entities = await repo.Query().ToListAsync();

            repo.UpdateHandled(entities);
        }
    }
}