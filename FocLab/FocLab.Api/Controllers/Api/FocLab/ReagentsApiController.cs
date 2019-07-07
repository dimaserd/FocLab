using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Reagents;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryReagents;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Reagents")]
    public class ReagentsApiController : BaseApiController
    {
        public ReagentsApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private ChemistryReagentsWorker ChemistryReagentsWorker => new ChemistryReagentsWorker(ContextWrapper);

        [HttpPost("CreateOrUpdate")]
        public Task<BaseApiResponse> CreateOrUpdateReagent(ChemistryReagentNameAndIdModel model)
        {
            return ChemistryReagentsWorker.CreateOrUpdateReagentAsync(model);
        }

        /// <summary>
        /// Получить задания на день
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ForTask/CreateOrUpdate")]
        public Task<BaseApiResponse> CreateOrUpdateForTask(CreateOrUpdateTaskReagent model)
        {
            return ChemistryReagentsWorker.CreateOrUpdateTaskReagentAsync(model);
        }

        /// <summary>
        /// Получить задания на день
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ForTask/Remove")]
        public Task<BaseApiResponse> RemoveForTask(TaskReagentIdModel model)
        {
            return ChemistryReagentsWorker.RemoveTaskReagentAsync(model);
        }
    }
}
