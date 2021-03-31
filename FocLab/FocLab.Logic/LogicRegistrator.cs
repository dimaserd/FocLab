using System.Linq;
using FocLab.Logic.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FocLab.Logic
{
    public static class LogicRegistrator
    {
        public static void Register(this IServiceCollection services)
        {
            RegisterFocLabWorkerTypes(services);
        }

        private static void RegisterFocLabWorkerTypes(IServiceCollection services)
        {
            var focLabWorkerTypes = typeof(FocLabWorker)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(FocLabWorker)) && !t.IsAbstract)
                .ToList();

            foreach (var focLabWorkerType in focLabWorkerTypes)
            {
                services.AddTransient(focLabWorkerType);
            }
        }
    }
}