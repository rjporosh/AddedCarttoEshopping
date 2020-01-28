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
using Microsoft.AspNetCore.Authentication.Facebook;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ecommerce.Models;

//using Microsoft.AspNetCore.Mvc.Formatters;

namespace Ecommerce.WebApp
{
    public  class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceDbContext>();


            services.AddIdentity<Ecommerce.Models.ApplicationUser, IdentityRole>()
                .AddDefaultUI()
                  .AddEntityFrameworkStores<EcommerceDbContext>()
                   .AddDefaultTokenProviders()
                ;
        
            services.AddAuthentication()
        .AddCookie(options => {
            options.LoginPath = "/Account/Unauthorized/";
            options.AccessDeniedPath = "/Account/Forbidden/";
        })
        .AddJwtBearer(options => {
            options.Audience = "http://localhost:5001/";
            options.Authority = "http://localhost:5000/";
        });
         //   services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");
           
            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddDefaultUI()
            //    .AddEntityFrameworkStores<EcommerceDbContext>();
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

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath ="/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "884097505482-j0tufq4fveclaqjalqhnfhj3hecl974o.apps.googleusercontent.com";
                    options.ClientSecret = "rvCRqaEYDZjDDlm9IhgWRpq0";
                })
                   .AddFacebook(options =>
                   {
                       options.AppId = "492985484738653";
                       options.AppSecret = "5db592e96a8b9e439ec0b8883d1321c6";
                   })
                   ;
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
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }
                )
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
            services.AddTransient<GetUserService>();
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

        //public void Configure(IApplicationBuilder app, ILoggerFactory loggerfactory)
        //{
        //    app.UseIdentity();
        //    app.UseAuthentication();
        //    app.UseFacebookAuthentication(new FacebookOptions
        //    {
        //        AppId = Configuration["auth:facebook:appid"],
        //        AppSecret = Configuration["auth:facebook:appsecret"]
        //    });
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
           
            app.UseStaticFiles();
            app.UseIdentity();
            //app.UseApplicationUser();
            //app.UseFacebookAuthentication(new FacebookOptions
            //{
            //    AppId = Configuration["auth:facebook:appid"],
            //    AppSecret = Configuration["auth:facebook:appsecret"]
            //});
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
