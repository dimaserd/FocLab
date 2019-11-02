using Hangfire.Dashboard;
using FocLab.Logic.Extensions;
using FocLab.Model.Enumerations;

namespace FocLab.Configuration.Hangfire
{
    public class MyDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var user = httpContext.User;

            //Разрешаем всем пользователям с данными правами доступ к дашборду
            return user.HasRight(UserRight.Developer) || user.IsAdmin();
        }
    }
}
