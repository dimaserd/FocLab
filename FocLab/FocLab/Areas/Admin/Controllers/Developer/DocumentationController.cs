using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Admin.Controllers.Developer
{
    public class DocumentationController : BaseController
    {
        public DocumentationController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        public IActionResult CodeGen()
        {
            return View();
        }
    }
}
