using Croco.Core.Abstractions;
using Croco.Core.Abstractions.Data;
using Croco.Core.Contract;
using Croco.Core.Contract.Data;
using Croco.Core.Data.Models;
using Croco.Core.Implementations;
using Croco.Core.Implementations.AmbientContext;
using Croco.WebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Principal;

namespace FocLab.Api.Controllers.Base
{
    public class FooActionFilter : IActionFilter, IOrderedFilter
    {
        private readonly Func<IPrincipal, string> _getUserIdFunc;

        public FooActionFilter(Func<IPrincipal, string> getUserIdFunc)
        {
            _getUserIdFunc = getUserIdFunc;
        }

        // Setting the order to 0, using IOrderedFilter, to attempt executing
        // this filter *before* the BaseController's OnActionExecuting.
        public int Order => 0;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            var principal = new WebAppCrocoPrincipal(httpContext.User, _getUserIdFunc);
            var requestContext = new CrocoRequestContext(principal);

            context.HttpContext.RequestServices
                .GetRequiredService<ICrocoRequestContextAccessor>()
                .SetRequestContext(requestContext);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

    /// <summary>
    /// Обобщенный веб-контроллер с основной логикой
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class CrocoGenericController<TContext, TUser> : Controller where TContext : DbContext where TUser : IdentityUser
    {
        private readonly Func<IPrincipal, string> _getUserIdFunc;

        /// <inheritdoc />
        public CrocoGenericController(ICrocoAmbientContextAccessor ambientContextAccessor)
        {
            Context = context;
            HttpContextAccessor = httpContextAccessor;
            _getUserIdFunc = getUserIdFunc;
            RequestContextAccessor = requestContextAccessor;
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
        protected ICrocoPrincipal CrocoPrincipal => ;


        public static string GetMimeMapping(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

            return contentType ?? "application/octet-stream";
        }

        /// <summary>
        /// Возвращает физический файл по пути и имени файла, из имени файла берется Mime тип
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected PhysicalFileResult PhysicalFileWithMimeType(string filePath, string fileName)
        {
            return PhysicalFile(filePath, GetMimeMapping(fileName), fileName);
        }

        #endregion
    }
}
