using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;

namespace FocLab.Areas.Chemistry.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Кастомный контроллер для приложения Химия
    /// </summary>
    public class CustomChemistryBaseApiController : BaseController
    {

        public CustomChemistryBaseApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}
