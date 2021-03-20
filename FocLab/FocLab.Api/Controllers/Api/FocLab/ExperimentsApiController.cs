using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.FocLab
{
    /// <summary>
    /// Апи контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Route("Api/Chemistry/Experiments")]
    public class ExperimentsApiController : Controller
    {
        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker { get; }

        public ExperimentsApiController(
            ChemistryTaskExperimentsWorker chemistryTaskExperimentsWorker)
        {
            ChemistryTaskExperimentsWorker = chemistryTaskExperimentsWorker;
        }

        /// <summary>
        /// Создать эксперимент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public Task<BaseApiResponse> Create(CreateExperiment model)
        {
            return ChemistryTaskExperimentsWorker.CreateExperimentForTaskAsync(model);
        }

        /// <summary>
        /// Обновить эксперимент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public Task<BaseApiResponse> Update(UpdateExperiment model)
        {
            return ChemistryTaskExperimentsWorker.UpdateExperimentAsync(model);
        }

        [HttpPost("Perform")]
        public Task<BaseApiResponse> Perform(PerformExperimentModel model)
        {
            return ChemistryTaskExperimentsWorker.PerformExperimentAsync(model);
        }

        [HttpPost("Remove")]
        public Task<BaseApiResponse> Remove(string id)
        {
            return ChemistryTaskExperimentsWorker.RemoveExperimentAsync(id);
        }

        [HttpPost("CancelRemove")]
        public Task<BaseApiResponse> CancelRemove(string id)
        {
            return ChemistryTaskExperimentsWorker.CancelRemovingExperimentAsync(id);
        }

        [HttpPost("ChangeFile")]
        public Task<BaseApiResponse> ChangeFile(ChemistryChangeFileForExperiment model)
        {
            return ChemistryTaskExperimentsWorker.LoadOrReloadFileForExperimentAsync(model);
        }
    }
}