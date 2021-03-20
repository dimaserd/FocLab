using Croco.Core.Contract;
using FocLab.Controllers.Base;
using FocLab.Logic.Settings.Statics;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc.Content
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер по умолчанию
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController(ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
        }

        /// <summary>
        /// Метод по умолчанию
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (WebAppSettings.UseMainRedirect)
            {
                return Redirect(WebAppSettings.RedirectUri);
            }

            return View();
        } 
    }
}