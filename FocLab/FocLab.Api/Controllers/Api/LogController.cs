using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Common.Models;
using Croco.Core.Logic.Workers.Log;
using Croco.WebApplication.Models.Exceptions;
using Croco.WebApplication.Models.Log;
using Croco.WebApplication.Workers.Log;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы логгирования
    /// </summary>
    [Route("Api/Log")]
    public class LogController : BaseApiController
    {
        /// <inheritdoc />
        public LogController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private ExceptionWorker ExceptionWorker => new ExceptionWorker(ContextWrapper);

        /// <summary>
        /// Залогировать исключения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Exceptions")]
        public async Task<BaseApiResponse> LogExceptions([FromForm]List<LogUserInterfaceException> model)
        {
            return await ExceptionWorker.LogUserInterfaceExceptionsAsync(model);
        }

        /// <summary>
        /// Залогировать одно исключение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Exception")]
        public async Task<BaseApiResponse> LogException([FromForm]LogUserInterfaceException model)
        {
            return await ExceptionWorker.LogUserInterfaceExceptionAsync(model);
        }

        /// <summary>
        /// Залогировать одно событие или действие
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Action")]
        public async Task<BaseApiResponse> LogAction([FromForm]LoggedUserInterfaceActionModel model)
        {
            var worker = new ActionLogWorker(ContextWrapper);

            return await worker.LogActionAsync(model);
        }

    }
}
