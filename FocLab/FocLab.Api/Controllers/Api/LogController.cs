using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Models;
using Croco.WebApplication.Models.Exceptions;
using Croco.WebApplication.Models.Log;
using Croco.WebApplication.Workers.Log;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Implementations;
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

        private ExceptionWorker<FocLabWebApplication> ExceptionWorker => new ExceptionWorker<FocLabWebApplication>(AmbientContext);

        /// <summary>
        /// Залогировать исключения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Exceptions")]
        public Task<BaseApiResponse> LogExceptions([FromForm]List<LogUserInterfaceException> model)
        {
            return ExceptionWorker.LogUserInterfaceExceptionsAsync(model);
        }

        /// <summary>
        /// Залогировать одно исключение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Exception")]
        public Task<BaseApiResponse> LogException([FromForm]LogUserInterfaceException model)
        {
            return ExceptionWorker.LogUserInterfaceExceptionAsync(model);
        }
    }
}