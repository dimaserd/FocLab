using System;
using System.Threading.Tasks;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using Croco.Core.Data.Abstractions;
using Croco.Core.Data.Models.ContextWrappers;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Zoo;

namespace FocLab.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый Mvc-контроллер
    /// </summary>
    public class BaseController : Controller
    {
        #region Конструкторы

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public BaseController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _context = context;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #endregion

        #region Поля



        /// <summary>
        /// Поле для менеджера ролей
        /// </summary>
        private RoleManager<ApplicationRole> _roleManager;

        private ChemistryDbContext _context;

        private UserContextWrapper<ChemistryDbContext> _contextWrapper;
        #endregion

        #region Свойства
        
        /// <summary>
        /// Класс предоставляющий методы для поиска пользователей
        /// </summary>
        protected UserSearcher UserSearcher => new UserSearcher(ContextWrapper);


        /// <summary>
        /// Отправитель почты
        /// </summary>
        protected IUserMailSender MailSender => null;

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        protected IApplicationAuthenticationManager AuthenticationManager => new ApplicationAuthenticationManager(SignInManager);

        
        /// <summary>
        /// Контекст для работы с бд
        /// </summary>
        public ChemistryDbContext Context => _context ?? (_context = CrocoApp.Application.GetChemistryDbContext());

        /// <summary>
        /// Менеджер для работы с ролями пользователей
        /// </summary>
        protected RoleManager<ApplicationRole> RoleManager => _roleManager ??
                                                           (_roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(Context), null, null, null, null));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get;
        }

        /// <summary>
        /// Менеджер для работы с пользователями
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get;
        }


        protected ICrocoPrincipal CrocoPrincipal => new MyCrocoPrincipal(User, x => x.Identity.GetUserId());

        /// <summary>
        /// Обёртка для контекста
        /// </summary>
        public UserContextWrapper<ChemistryDbContext> ContextWrapper => _contextWrapper ?? (_contextWrapper = new UserContextWrapper<ChemistryDbContext>(CrocoPrincipal, Context));

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

        #region Вспомогательные методы

        #region AddErrors

        /// <summary>
        /// Добавить ошибки к модели
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        protected void AddErrors<T>(BaseApiResponse<T> result) where T : class
        {
            if (!result.IsSucceeded)
            {
                ModelState.AddModelError("", result.Message);
            }
        }

        /// <summary>
        /// Добавить ошибки к модели
        /// </summary>
        /// <param name="result"></param>
        protected void AddErrors(BaseApiResponse result)
        {
            if (!result.IsSucceeded)
            {
                ModelState.AddModelError("", result.Message);
            }
        }

        /// <summary>
        /// Добавить ошибки к модели
        /// </summary>
        /// <param name="result"></param>
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        /// <summary>
        /// Добавить ошибки к модели
        /// </summary>
        /// <param name="result"></param>
        protected void AddErrors(Tuple<bool, string, string> result)
        {
            ModelState.AddModelError("", result.Item2);
        }

        #endregion

        #endregion

        protected IActionResult HttpNotFound()
        {
            return StatusCode(404);
        }

        /// <inheritdoc />
        /// <summary>
        /// Метод уничтожения объекта Controller
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var toDisposes = new IDisposable[]
                {
                    UserManager, _context, _roleManager
                };

                for (var i = 0; i < toDisposes.Length; i++)
                {
                    if (toDisposes[i] == null)
                    {
                        continue;
                    }
                    toDisposes[i].Dispose();
                    toDisposes[i] = null;

                }
            }

            base.Dispose(disposing);
        }
    }
}
