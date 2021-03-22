using Microsoft.Extensions.DependencyInjection;
using Tms.Logic.Services;

namespace Tms.Logic
{
    public static class TmsRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<DayTasksService>();
        }
    }
}