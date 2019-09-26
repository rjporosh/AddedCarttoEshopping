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
//using Microsoft.AspNetCore.Mvc.Formatters;

namespace Ecommerce.WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ServicesConfiguration.ConfigureServices(services);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc();
            //services.AddMvc().AddMvcOptions
            //    (options =>
            //    {
            //        options.RespectBrowserAcceptHeader = true;
            //        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            //    })

            //   .AddJsonOptions(
            //        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );
             services.AddMvc(options =>
    {
        options.FormatterMappings.SetMediaTypeMappingForFormat
            ("xml", MediaTypeHeaderValue.Parse("application/xml"));
        options.FormatterMappings.SetMediaTypeMappingForFormat
            ("config", MediaTypeHeaderValue.Parse("application/xml"));
        options.FormatterMappings.SetMediaTypeMappingForFormat
            ("js", MediaTypeHeaderValue.Parse("application/json"));
    })
        .AddXmlSerializerFormatters();
            services.AddSession();


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
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}"
                    );
                routes.MapRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
