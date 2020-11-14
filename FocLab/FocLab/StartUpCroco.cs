using Croco.Core.Abstractions;
using Croco.Core.Application;
using Croco.Core.Application.Options;
using Croco.Core.Common.Enumerations;
using Croco.Core.Hangfire.Extensions;
using Croco.Core.Logic.Models.Files;
using Croco.WebApplication.Application;
using FocLab.Implementations;
using FocLab.Implementations.StateCheckers;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FocLab
{
    public class StartUpCrocoOptions
    {
        public IConfiguration Configuration { get; set; }

        public IHostingEnvironment Env { get; set; }

        public List<Action<ICrocoApplication>> ApplicationActions { get; set; } = new List<Action<ICrocoApplication>>();
    }

    public class StartupCroco
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        public List<Action<ICrocoApplication>> ApplicationActions { get; }

        public StartupCroco(StartUpCrocoOptions options)
        {
            Configuration = options.Configuration;
            Env = options.Env;
            ApplicationActions = options.ApplicationActions;
        }

        public void SetCrocoApplication(IServiceCollection services)
        {
            var memCache = new MemoryCache(new MemoryCacheOptions());

            services.AddSingleton<IMemoryCache, MemoryCache>(s => memCache);

            //Добавляю проверщика состояния миграций
            ApplicationActions.Add(CrocoMigrationStateChecker.CheckApplicationState);

            var conString = Configuration.GetConnectionString(ChemistryDbContext.ConnectionString);

            var appOptions = new CrocoWebApplicationOptions
            {
                ApplicationUrl = "https://foc-lab.com",
                VirtualPathMapper = new ApplicationServerVirtualPathMapper(Env),
                CacheManager = new ApplicationCacheManager(memCache),
                GetDbContext = () => ChemistryDbContext.Create(conString),
                RequestContextLogger = new CrocoWebAppRequestContextLogger(),
                FileOptions = new CrocoFileOptions
                {
                    SourceDirectory = Env.WebRootPath,
                    ImgFileResizeSettings = new List<ImgFileResizeSetting>
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
                    },
                },
                AfterInitActions = ApplicationActions
            }.AddHangfireEventSourcerAndJobManager();

            var application = new FocLabWebApplication(appOptions)
            {
                IsDevelopment = Env.IsDevelopment()
            };

            CrocoApp.Application = application;

            services.AddSingleton(CrocoApp.Application);
        }
    }
}