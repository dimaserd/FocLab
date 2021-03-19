using System.Threading.Tasks;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый Mvc-контроллер
    /// </summary>
    public class BaseController : CrocoGenericController<ChemistryDbContext, ApplicationUser>
    {
        public BaseController(ChemistryDbContext context, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager) : base(context, signInManager, userManager, x => x.GetUserId(), null)
        {
        }

        /// <summary>
        /// Поле для менеджера ролей
        /// </summary>
        private RoleManager<ApplicationRole> _roleManager;

        #region Свойства
        
        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        protected IApplicationAuthenticationManager AuthenticationManager => new ApplicationAuthenticationManager(SignInManager);

        /// <summary>
        /// Менеджер для работы с ролями пользователей
        /// </summary>
        protected RoleManager<ApplicationRole> RoleManager => _roleManager ??= new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(Context), null, null, null, null);

        
        private ApplicationUser _currentUser;

        

        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            if (_currentUser == null)
            {
                _currentUser = await UserManager.GetUserAsync(User);
            }

            return _currentUser;
        }

        /// <summary>
        /// Идентификатор текущего залогиненного пользователя
        /// </summary>
        protected string UserId => User.Identity.GetUserId();

        #endregion

        protected IActionResult HttpNotFound()
        {
            return StatusCode(404);
        }
    }
}