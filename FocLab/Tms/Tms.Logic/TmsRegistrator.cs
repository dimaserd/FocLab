using Microsoft.Extensions.DependencyInjection;
using Tms.Logic.Workers.Tasker;

namespace Tms.Logic
{
    public static class TmsRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<DayTasksWorker>();
        }
    }
}