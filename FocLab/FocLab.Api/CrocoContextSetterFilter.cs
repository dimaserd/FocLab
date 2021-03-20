using Croco.Core.Contract;
using Croco.Core.Data.Models;
using Croco.WebApplication.Models;
using FocLab.Logic.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FocLab.Api
{
    public class CrocoContextSetterFilter : IActionFilter, IOrderedFilter
    {
        // Setting the order to 0, using IOrderedFilter, to attempt executing
        // this filter *before* the BaseController's OnActionExecuting.
        public int Order => int.MinValue;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            var principal = new WebAppCrocoPrincipal(httpContext.User, x => x.GetUserId());
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