using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Api.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый абстрактный контроллер в котором собраны общие методы и свойства
    /// </summary>
    public class BaseApiController : CrocoGenericController<ChemistryDbContext, ApplicationUser>
    {
        /// <inheritdoc />
        public BaseApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, x => x.GetUserId(), httpContextAccessor)
        {
        }

        #region Поля

        //TODO Impelement RoleManager
        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public RoleManager<IdentityRole> RoleManager = null;
        #endregion

        #region Свойства

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        protected IApplicationAuthenticationManager AuthenticationManager => new ApplicationAuthenticationManager(SignInManager);

        /// <summary>
        /// Идентификатор текущего залогиненного пользователя
        /// </summary>
        protected string UserId => User.Identity.GetUserId();

        #endregion
    } 
}
