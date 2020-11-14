using System;
using FocLab.Extensions;
using FocLab.Implementations;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using FocLab.Logic.Extensions;
using FocLab.Configuration.Hangfire;
using FocLab.Configuration.Swagger;
using System.Collections.Generic;

namespace FocLab
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Croco = new StartupCroco(new StartUpCrocoOptions 
            {
                Configuration = configuration,
                Env = env,
            });
        }

        public StartupCroco Croco { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            Croco.SetCrocoApplication(services);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString(ChemistryDbContext.ConnectionString)));

            services.AddTransient<ApplicationUserManager>();
            services.AddTransient<ApplicationSignInManager>();

            services.AddDbContext<ChemistryDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(ChemistryDbContext.ConnectionString)));


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

            services.AddSignalR().AddJsonProtocol(options => ConfigureJsonSerializer(options.PayloadSerializerSettings));

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => ConfigureJsonSerializer(options.SerializerSettings));

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                //TODO Удалить когда будут убраны все баги
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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "areas",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.ConfigureExceptionHandler(new ApplicationLoggerManager());

            HangfireConfiguration.AddHangfire(app, false);
        }

        private static void ConfigureJsonSerializer(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new DefaultContractResolver();
            settings.Converters.Add(new StringEnumConverter());
        }
    }
}