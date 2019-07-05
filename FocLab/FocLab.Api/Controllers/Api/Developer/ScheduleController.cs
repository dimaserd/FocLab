using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Jobs;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Developer
{
    /// <inheritdoc />
    /// <summary>
    /// Расписание
    /// </summary>
    [Route("Api/Schedule")]
    public class ScheduleController : BaseApiController
    {
        /// <inheritdoc />
        public ScheduleController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }


        /// <summary>
        /// Добавление отложенной задачи
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(AddJob))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public BaseApiResponse AddJob()
        {
            ApplicationJobManager.UpdateJobs();

            return new BaseApiResponse(true, "Добавлено");
        }
    }
}
