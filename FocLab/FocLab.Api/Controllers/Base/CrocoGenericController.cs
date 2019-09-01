using Croco.Core.Abstractions;
using Croco.Core.Data.Abstractions;
using Croco.Core.Implementations.AmbientContext;
using Croco.WebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Principal;

namespace FocLab.Api.Controllers.Base
{
    public class CrocoGenericController<TContext, TUser> : Controller where TContext : DbContext where TUser : IdentityUser
    {
        /// <inheritdoc />
        public CrocoGenericController(TContext context, SignInManager<TUser> signInManager, UserManager<TUser> userManager, Func<IPrincipal, string> getUserIdFunc, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            UserManager = userManager;
            SignInManager = signInManager;
            HttpContextAccessor = httpContextAccessor;
            CrocoPrincipal = new WebAppCrocoPrincipal(User, getUserIdFunc);
            RequestContext = new WebAppCrocoRequestContext(CrocoPrincipal);
            AmbientContext = new CrocoAmbientContext(Context, RequestContext);
        }

        #region Свойства

        /// <summary>
        /// Контекст для работы с бд
        /// </summary>
        public TContext Context
        {
            get;
        }

        /// <summary>
        /// Контекст текущего пользователя
        /// </summary>
        protected ICrocoPrincipal CrocoPrincipal { get; }

        /// <summary>
        /// Обёртка для контекста окружения
        /// </summary>
        public ICrocoAmbientContext AmbientContext { get; }

        /// <summary>
        /// Контекст текущего запроса
        /// </summary>
        protected IRequestContext RequestContext { get; }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        public SignInManager<TUser> SignInManager
        {
            get;
            set;
        }

        /// <summary>
        /// Менеджер для работы с пользователями
        /// </summary>
        public UserManager<TUser> UserManager
        {
            get;
            set;
        }

        /// <summary>
        /// Контекст доступа к запросу
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }

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
