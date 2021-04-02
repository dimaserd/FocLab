using Croco.Core.Application;
using Croco.Core.Common.Enumerations;
using Croco.Core.Contract.Application.Common;
using Croco.Core.Contract.Cache;
using Croco.Core.Contract.Files;
using Croco.WebApplication.Application;
using FocLab.App.Logic.Implementations;
using FocLab.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CrocoShop.CrocoStuff
{
    public class StartupCroco
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public StartupCroco(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public CrocoApplicationBuilder SetCrocoApplication(IServiceCollection services)
        {
            var memCache = new MemoryCache(new MemoryCacheOptions());

            services.AddSingleton<IMemoryCache, MemoryCache>(s => memCache);
            services.AddSingleton<ICrocoCacheManager, ApplicationCacheManager>();

            var appBuilder = new CrocoApplicationBuilder(services);
            
            appBuilder.RegisterFileStorage(new CrocoFileOptions
            {
                SourceDirectory = Env.WebRootPath ?? $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files",
                ImgFileResizeSettings = new ImgFileResizeSetting[]
                {
                    new ImgFileResizeSetting
                    {
                        ImageSizeName = ImageSizeType.Icon.ToString(),
                        MaxHeight = 50,
                        MaxWidth = 50
                    },
                    new ImgFileResizeSetting
                    {
                        ImageSizeName = ImageSizeType.Small.ToString(),
                        MaxHeight = 200,
                        MaxWidth = 200
                    },
                    new ImgFileResizeSetting
                    {
                        ImageSizeName = ImageSizeType.Medium.ToString(),
                        MaxHeight = 500,
                        MaxWidth = 500
                    }
                }
            });
            appBuilder.RegisterVirtualPathMapper(Env.ContentRootPath);

            var options = Configuration.GetSection("FocLabOptions").Get<CrocoWebApplicationOptions>();

            services.AddSingleton(options);

            appBuilder.CheckAndRegisterApplication<FocLabWebApplication>();
            
            return appBuilder;
        }
    }
}