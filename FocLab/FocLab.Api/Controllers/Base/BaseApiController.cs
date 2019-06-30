using System;
using Croco.Core.ContextWrappers;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый абстрактный контроллер в котором собраны общие методы и свойства
    /// </summary>
    public class BaseApiController : Controller
    {
        /// <inheritdoc />
        public BaseApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            SignInManager = signInManager;
            UserManager = userManager;
            HttpContextAccessor = httpContextAccessor;
        }

        #region Поля

        //TODO Impelement RoleManager
        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public RoleManager<IdentityRole> RoleManager = null;
        
        private UserContextWrapper<ChemistryDbContext> _contextWrapper;
        #endregion

        #region Свойства

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        protected IApplicationAuthenticationManager AuthenticationManager => new ApplicationAuthenticationManager(SignInManager);

        /// <summary>
        /// Контекст для работы с бд
        /// </summary>
        public ChemistryDbContext Context 
        {
            get;
        }

        /// <summary>
        /// Обёртка для контекста
        /// </summary>
        public UserContextWrapper<ChemistryDbContext> ContextWrapper => _contextWrapper ?? (_contextWrapper = new UserContextWrapper<ChemistryDbContext>(User, Context, x => x.Identity.GetUserId()));

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get;
            set;
        }

        /// <summary>
        /// Менеджер для работы с пользователями
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get;
            set;
        }

        /// <summary>
        /// Контекст доступа к запросу
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }


        /// <summary>
        /// Идентификатор текущего залогиненного пользователя
        /// </summary>
        protected string UserId => User.Identity.GetUserId();

        #endregion
        
        /// <inheritdoc />
        /// <summary>
        /// Удаление объекта из памяти
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var toDisposes = new IDisposable[]
                {
                    UserManager, Context
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
