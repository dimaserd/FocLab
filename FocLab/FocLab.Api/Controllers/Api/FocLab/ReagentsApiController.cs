using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Reagents;
using FocLab.Logic.Workers.ChemistryReagents;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Reagents")]
    public class ReagentsApiController : BaseApiController
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker { get; }

        public ReagentsApiController(ICrocoRequestContextAccessor requestContextAccessor,
            ChemistryReagentsWorker chemistryReagentsWorker) : base(requestContextAccessor)
        {
            ChemistryReagentsWorker = chemistryReagentsWorker;
        }


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