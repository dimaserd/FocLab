using Microsoft.Extensions.DependencyInjection;
using MigrationTool.Tools;

namespace MigrationTool
{
    public static class MigrationToolRegistator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<AddDbFileHistory>();
            services.AddTransient<AddSnaphotsForEntities>();
        }
    }
}