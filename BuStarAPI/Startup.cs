using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuStarAPI.Extensions;
using BuStarAPI.Repository;
using BuStarAPI.Scheduling;
using BuStarAPI.Services;
using BuStarAPI.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuStarAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IScheduledTask, DownloadTristarDataTask>();
            services.AddScheduler((sender, args)=>
            {
                Console.Write(args.Exception.Message);
                args.SetObserved();
            });

            services.AddSingleton<IDataParseService,DataParseService>();
            services.AddSingleton<IRepository>(dataMapper => new BuStarRepository(Configuration.GetConnectionString("DefaultConnection")
            , new DataParseService()
            , new DateTimeService()));

            services.AddCors(o => o.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("EnableCors");
        }
    }
}