using Croco.Core.Contract.Models;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Workers.ChemistryMethods;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Methods")]
    public class MethodsApiController : Controller
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker { get; }

        public MethodsApiController(ChemistryMethodsWorker chemistryMethodsWorker)
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