﻿using System;
using FocLab.Extensions;
using FocLab.Implementations;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
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
using FocLab.Api;
using FocLab.Logic.Extensions;
using FocLab.Abstractions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Abstractions;
using Croco.Core.Application;
using Zoo.GenericUserInterface.Models.Overridings;
using FocLab.InterfaceDefinitions;

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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

            var conString = Configuration.GetConnectionString(ChemistryDbContext.ConnectionString);

            services.AddTransient<ApplicationUserManager>();
            services.AddTransient<ApplicationSignInManager>();

            services.AddDbContext<ChemistryDbContext>(options =>
                options.UseSqlServer(conString));

            services.AddIdentity<ApplicationUser, ApplicationRole>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ChemistryDbContext>();

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
            services.AddSingleton<IUserMailSender, FocLabEmailSender>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new CrocoContextSetterFilter(x => x.GetUserId()));
            });
            ApplicationBuilder = Croco.SetCrocoApplication(services);

            new GenericUserInterfaceBagBuilder(services)
                .AddDefaultDefinition<CreateUserModelUserInterfaceDefinition>()
                .AddDefaultDefinition<EditApplicationUserInterfaceDefinition>()
                .AddDefaultDefinition<CreateOrUpdateDayTaskInterfaceDefinition>()
                .AddDefaultDefinition<CreateExperimentInterfaceDefinition>()
                .Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}