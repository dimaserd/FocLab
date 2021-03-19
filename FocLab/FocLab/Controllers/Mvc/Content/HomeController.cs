using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc.Content
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер по умолчанию
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
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