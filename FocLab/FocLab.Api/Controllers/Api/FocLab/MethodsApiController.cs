using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Workers.ChemistryMethods;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Methods")]
    public class MethodsApiController : BaseApiController
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker { get; }

        public MethodsApiController(ICrocoRequestContextAccessor requestContextAccessor,
            ChemistryMethodsWorker chemistryMethodsWorker) : base(requestContextAccessor)
        {
            ChemistryMethodsWorker = chemistryMethodsWorker;
        }

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public Task<BaseApiResponse> Create(CreateChemistryMethod model)
        {
            return ChemistryMethodsWorker.CreateMethodAsync(model);
        }

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public Task<BaseApiResponse> Update(EditChemistryMethod model)
        {
            return ChemistryMethodsWorker.EditMethodAsync(model);
        }
    }
}