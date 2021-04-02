using System;
using FocLab.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FocLab.Configuration.Swagger;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CrocoShop.CrocoStuff;
using FocLab.Abstractions;
using Croco.Core.Application;
using Zoo.GenericUserInterface.Models.Overridings;
using FocLab.InterfaceDefinitions;
using Croco.WebApplication.Models;
using Croco.Core.Data.Models;
using Croco.Core.Contract;
using Tms.Model;
using FocLab.Model;
using Clt.Model;
using Croco.Core.Logic.DbContexts;
using FocLab.App.Logic;
using FocLab.App.Logic.Extensions;
using FocLabWebApp.Helpers;
using FocLabWebApp.Implementations;

namespace FocLab
{
    public class Startup
    {
        StartupCroco Croco { get; }
        CrocoApplicationBuilder ApplicationBuilder { get; set; }
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Croco = new StartupCroco(configuration, env);
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddControllersAsServices();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
                options.ValueCountLimit = 200; // 200 items max
                options.ValueLengthLimit = 1024 * 1024 * 100; // 100MB max len form data
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            SwaggerConfiguration.ConfigureSwagger(services, new List<string>
            {
            });

            services.AddSignalR().AddJsonProtocol(options => ConfigureJsonSerializer(options.PayloadSerializerOptions));

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddControllersWithViews().AddJsonOptions(options => ConfigureJsonSerializer(options.JsonSerializerOptions));
            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<ILoggerManager, ApplicationLoggerManager>();

            RegiterDbContexts(services);
            ApplicationBuilder = Croco.SetCrocoApplication(services);

            LogicRegistrator.Register(ApplicationBuilder);
            
            new GenericUserInterfaceBagBuilder(services)
                .AddDefaultDefinition<EditApplicationUserInterfaceDefinition>()
                .AddDefaultDefinition<CreateOrUpdateDayTaskInterfaceDefinition>()
                .AddDefaultDefinition<CreateExperimentInterfaceDefinition>()
                .Build();

            services.AddTransient<ChemistryTasksHtmlHelper>();
        }

        public void RegiterDbContexts(IServiceCollection services)
        {
            services.AddDbContext<FocLabDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("FocLabDbConnection"), b => b.MigrationsAssembly("FocLabWebApp")));

            services.AddDbContext<TmsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TmsDbConnection"), b => b.MigrationsAssembly("FocLab.App.Model")));

            services.AddDbContext<CltDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CltDbConnection"), b => b.MigrationsAssembly("FocLab.App.Model")));

            services.AddDbContext<CrocoInternalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CrocoDbConnection"), b => b.MigrationsAssembly("FocLab.App.Model")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            TmsDbContext tmsDbContext,
            FocLabDbContext focLabDbContext,
            CltDbContext cltDbContext,
            CrocoInternalDbContext internalDbContext)
        {
            tmsDbContext.Database.Migrate();
            focLabDbContext.Database.Migrate();
            cltDbContext.Database.Migrate();
            internalDbContext.Database.Migrate();

            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use((context, next) =>
            {
                OnActionExecuting(context);
                // Do work that doesn't write to the Response.
                return next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapAreaControllerRoute(
                    "admin",
                    "admin",
                    "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "chemistry",
                    "chemistry",
                    "Chemistry/{controller=Home}/{action=Index}/{id?}");

            });

            app.ConfigureExceptionHandler();
            ApplicationBuilder.SetAppAndActivator(app.ApplicationServices);
        }

        private static void ConfigureJsonSerializer(JsonSerializerOptions settings)
        {
            settings.PropertyNameCaseInsensitive = true;
            settings.PropertyNamingPolicy = null;
            settings.Converters.Add(new JsonStringEnumConverter());
        }

        private static void OnActionExecuting(HttpContext httpContext)
        {
            var requestServices = httpContext.RequestServices;

            var httpContextAccessor = requestServices
                .GetRequiredService<IHttpContextAccessor>();

            var principal = new WebAppCrocoPrincipal(httpContextAccessor.HttpContext.User, x => x.GetUserId());
            var requestContext = new CrocoRequestContext(principal);

            requestServices
                .GetRequiredService<ICrocoRequestContextAccessor>()
                .SetRequestContext(requestContext);
        }
    }
}