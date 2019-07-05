using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Experiments")]
    public class ExperimentsApiController : BaseApiController
    {
        public ExperimentsApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker => new ChemistryTaskExperimentsWorker(ContextWrapper);

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Create(CreateExperiment model)
        {
            return ChemistryTaskExperimentsWorker.CreateExperimentForTaskAsync(model);
        }
    }
}
