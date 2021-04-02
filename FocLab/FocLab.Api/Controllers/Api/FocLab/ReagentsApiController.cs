using Croco.Core.Contract.Models;
using FocLab.Logic.Models.Reagents;
using FocLab.Logic.Services.ChemistryReagents;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Reagents")]
    public class ReagentsApiController : Controller
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker { get; }

        public ReagentsApiController(ChemistryReagentsWorker chemistryReagentsWorker)
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