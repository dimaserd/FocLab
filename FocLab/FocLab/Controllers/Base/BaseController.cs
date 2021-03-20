using Croco.Core.Contract;
using FocLab.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FocLab.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый Mvc-контроллер
    /// </summary>
    public class BaseController : BaseApiController
    {
        public BaseController(ICrocoRequestContextAccessor requestContextAccessor,
            IActionContextAccessor actionContextAccessor) : 
            base(requestContextAccessor, actionContextAccessor)
        {
        }

        protected IActionResult HttpNotFound()
        {
            return StatusCode(404);
        }
    }
}