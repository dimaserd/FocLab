using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Jobs;
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
        public ScheduleController(ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
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