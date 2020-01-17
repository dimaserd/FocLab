using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Logic.Implementations;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class AddSnaphotsForEntities : FocLabWorker
    {
        public AddSnaphotsForEntities(ICrocoAmbientContext context) : base(context)
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
