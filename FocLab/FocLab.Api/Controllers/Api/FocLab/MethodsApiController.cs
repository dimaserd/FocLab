using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Methods;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Chemistry/Methods")]
    public class MethodsApiController : BaseApiController
    {
        public MethodsApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private ChemistryMethodsWorker ChemistryMethodsWorker => new ChemistryMethodsWorker(ContextWrapper);

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
