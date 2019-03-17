using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration configuration;

        //public Startup(IHostingEnvironment hostingEnvironment)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(hostingEnvironment.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange:true);

        //    configuration = builder.Build();
        //}
        public Startup(IConfiguration conf)
        {
            configuration = conf;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //    .AddJsonOptions(o=> {
            //    if (o.SerializerSettings.ContractResolver != null) {
            //        var custResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        custResolver.NamingStrategy = null;
            //    }
            //});

            services.AddTransient<IMailService, CloudMailService>();
            var conStr = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<CityInfoContext>(o=>o.UseSqlServer(conStr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            //loggerFactory.AddConsole();

            //loggerFactory.AddDebug();
            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            //loggerFactory.AddNLog();
                
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            cityInfoContext.EnsureSeedDataForDB();
            app.UseMvc();


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });


        }
    }
}
