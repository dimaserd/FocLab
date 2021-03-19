using Croco.Core.Contract;
using Croco.Core.Data.Models;
using Croco.WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Principal;

namespace FocLab.Api
{
    public class CrocoContextSetterFilter : IActionFilter, IOrderedFilter
    {
        private readonly Func<IPrincipal, string> _getUserIdFunc;

        public CrocoContextSetterFilter(Func<IPrincipal, string> getUserIdFunc)
        {
            _getUserIdFunc = getUserIdFunc;
        }

        // Setting the order to 0, using IOrderedFilter, to attempt executing
        // this filter *before* the BaseController's OnActionExecuting.
        public int Order => int.MinValue;

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
}