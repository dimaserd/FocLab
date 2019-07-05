using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Areas.Chemistry.Controllers.Api
{
    [Route("Api/Chemistry/Methods")]
    public class MethodsApiController : BaseFocLabApiController
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker => new ChemistryMethodsWorker(ContextWrapper);


        public MethodsApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
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

        
    }
}
