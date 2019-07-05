using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;

namespace FocLab.Areas.Chemistry.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class BaseFocLabController : BaseController
    {
        public BaseFocLabController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}