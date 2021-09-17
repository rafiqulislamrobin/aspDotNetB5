using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataImporter.Common;
using DataImporter.Data;
using DataImporter.Info;
using DataImporter.Info.Context;
using DataImporter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter
{
    public class Startup
    {
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfiguration Configuration { get; }
        public static ILifetimeScope AutofacContainer { get; set; }



        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings{env.EnvironmentName}", optional: true)
                .AddEnvironmentVariables();

            WebHostEnvironment = env;
            Configuration = builder.Build();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();
            builder.RegisterModule(new DataImporterModule
                (connectionInfo.connectionString, connectionInfo.migrationAssemblyName));

            builder.RegisterModule(new CommonModule());
            //builder.RegisterModule(new MemberShipModule
            //    (connectionInfo.connectionString, connectionInfo.migrationAssemblyName));

            builder.RegisterModule(new WebModule());
        }

        private (string connectionString, string migrationAssemblyName) GetConnectionStringAndAssemblyName()
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            return (connectionString, migrationAssemblyName);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = GetConnectionStringAndAssemblyName();

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(connectionInfo.connectionString,
                  b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddTransient<IEmailService, EmailSender>();
            services.AddTransient<IRecaptchaService, RecaptchaService>();


            services.AddDbContext<DataImporterDbContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                  b => b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            //// Identity customization started here
            //services
            //    .AddIdentity<ApplicationUser, Role>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddUserManager<UserManager>()
            //    .AddRoleManager<RoleManager>()
            //    .AddSignInManager<SignInManager>()
            //    .AddDefaultUI()
            //    .AddDefaultTokenProviders();


         
           
            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Signin";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //services.Configure<SmtpConfiguration>(Configuration.GetSection("Smtp"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
         
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                name: "default",
                areaName: "User",
                pattern: "{controller=Account}/{action=Login}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Account}/{action=Login}/{Id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
