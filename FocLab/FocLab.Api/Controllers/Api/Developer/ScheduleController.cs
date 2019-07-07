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
        /// Взять значение джоб по-умолчанию из кода
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(UpdateJobs))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public BaseApiResponse UpdateJobs()
        {
            ApplicationJobManager.UpdateJobs();

            return new BaseApiResponse(true, "Добавлено");
        }

        /// <summary>
        /// Взять значение джоб по-умолчанию из кода
        /// </summary>
        /// <returns></returns>
        [HttpPost("RemoveJobs")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public BaseApiResponse RemoveJobs()
        {
            ApplicationJobManager.RemoveJobs();

            return new BaseApiResponse(true, "Удалены");
        }
    }
}
