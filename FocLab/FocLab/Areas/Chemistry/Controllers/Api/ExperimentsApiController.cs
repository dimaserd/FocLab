using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Api
{
    /// <summary>
    /// Апи контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Route("Api/Chemistry/Experiments")]
    public class ExperimentsApiController : CustomChemistryBaseApiController
    {
        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker =>
            new ChemistryTaskExperimentsWorker(ContextWrapper);

        /// <summary>
        /// Получить задания на день
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateTitle")]
        public async Task<BaseApiResponse> UpdateTitle(ChemistryTaskExperimentDto model)
        {
            return await ChemistryTaskExperimentsWorker.UpdateExperimentTitleAsync(model);
        }

        public ExperimentsApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}
