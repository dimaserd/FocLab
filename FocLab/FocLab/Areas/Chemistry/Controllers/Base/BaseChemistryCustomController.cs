using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;

namespace FocLab.Areas.Chemistry.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class BaseChemistryCustomController : BaseController
    {
        public BaseChemistryCustomController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}