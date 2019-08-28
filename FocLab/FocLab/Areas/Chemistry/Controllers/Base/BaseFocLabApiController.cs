using FocLab.Api.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using FocLab.Logic.Extensions;

namespace FocLab.Areas.Chemistry.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Кастомный контроллер для приложения Химия
    /// </summary>
    public class BaseFocLabApiController : CrocoGenericController<ChemistryDbContext, ApplicationUser>
    {

        public BaseFocLabApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, signInManager, userManager, x => x.GetUserId(), null)
        {
        }
    }
}
