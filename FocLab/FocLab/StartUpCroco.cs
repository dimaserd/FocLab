using Croco.Core.Application;
using Croco.Core.Application.Options;
using Croco.Core.Common.Enumerations;
using Croco.Core.EventSourcing.Abstractions;
using Croco.Core.EventSourcing.Implementations;
using Croco.Core.Logic.Models.Files;
using Croco.WebApplication.Application;
using Ecc.Logic.RegistrationModule;
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
using System.Linq;
using System.Threading.Tasks;

namespace FocLab
{
    public class StartupCroco
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        public StartupCroco(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        private ICrocoEventSourcer GetEventSourcer()
        {
            return new ApplicationCrocoEventSourcer(GetEventPublisher());
        }

        private ICrocoEventPublisher GetEventPublisher()
        {
            var options = new CrocoEventListenerOptions
            {
                TaskEnqueuer = new HangfireTaskEnqueuer(),
            };

            var evListener = new CrocoEventListener(options);

            //Подписка обработчиками на события
            EccEventsSubscription.Subscribe(evListener);

            var publisher = new CrocoEventPublisher(new CrocoEventPublisherOptions
            {
                EventListener = evListener
            });

            return publisher;
        }

        public void SetCrocoApplication(IServiceCollection services)
        {
            var memCache = new MemoryCache(new MemoryCacheOptions());

            services.AddSingleton<IMemoryCache, MemoryCache>(s => memCache);

            var appOptions = new CrocoWebApplicationOptions(new ApplicationServerVirtualPathMapper(Env))
            {
                EventSourcer = GetEventSourcer(),
                CacheManager = new ApplicationCacheManager(memCache),
                GetDbContext = () => ChemistryDbContext.Create(Configuration),
                //Устнавливаю проверщики состояния приложения
                StateCheckers = new[]
                {
                    new CrocoMigrationStateChecker()
                },
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
                    }
                }
            };

            var application = new FocLabWebApplication(appOptions)
            {
                IsDevelopment = Env.IsDevelopment(),
            };

            CrocoApp.Application = application;

            services.AddSingleton(CrocoApp.Application);
        }
    }
}
