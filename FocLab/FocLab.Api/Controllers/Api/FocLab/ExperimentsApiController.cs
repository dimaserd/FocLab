using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.FocLab
{
    /// <summary>
    /// Апи контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Route("Api/Chemistry/Experiments")]
    public class ExperimentsApiController : BaseApiController
    {
        public ExperimentsApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker =>
            new ChemistryTaskExperimentsWorker(ContextWrapper);


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
