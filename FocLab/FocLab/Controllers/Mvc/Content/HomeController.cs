using FocLab.Logic.Settings.Statics;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc.Content
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер по умолчанию
    /// </summary>
    public class HomeController : Controller
    {
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