using Croco.Core.Contract;
using FocLab.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый Mvc-контроллер
    /// </summary>
    public class BaseController : BaseApiController
    {
        public BaseController(ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
        }

        protected IActionResult HttpNotFound()
        {
            return StatusCode(404);
        }
    }
}