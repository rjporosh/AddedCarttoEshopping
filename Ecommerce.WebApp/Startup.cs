using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL;
using Ecommerce.Configurations;
using Ecommerce.DatabaseContext;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Abstractions.Helper;
using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Mvc.Formatters;

namespace Ecommerce.WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceDbContext>();


          

            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<EcommerceDbContext>();
            services.ConfigureServicesForEcommerce();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;


                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath ="/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                });
            });
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.Configure<MvcJsonOptions>(config =>
            {
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSession();
            //services.AddMvc().AddMvcOptions
            //    (options =>
            //    {
            //        options.RespectBrowserAcceptHeader = true;
            //        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //    })

            //   .AddJsonOptions(
            //        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );
            //services.AddSingleton<ITotalQuantity,TotalItemService >();
            services.AddTransient<TotalItemService>();
            services.AddMvc(options =>
                   {
                       options.FormatterMappings.SetMediaTypeMappingForFormat
                            ("xml", MediaTypeHeaderValue.Parse("application/xml"));
                       options.FormatterMappings.SetMediaTypeMappingForFormat
                            ("config", MediaTypeHeaderValue.Parse("application/xml"));
                       options.FormatterMappings.SetMediaTypeMappingForFormat
                           ("js", MediaTypeHeaderValue.Parse("application/json"));
                       
                   })
                     .AddXmlSerializerFormatters()
                     .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore );
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
           
            app.UseStaticFiles();
          
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseCors("AllowAll");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=_cardView}"
                    );
                routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
