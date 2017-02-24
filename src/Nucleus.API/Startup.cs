using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nucleus.API.Entities;
using Nucleus.API.Models;
using Nucleus.API.Repositories;

namespace Nucleus.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=NucleusDb;Trusted_Connection=True;";
            services.AddDbContext<NucleusDbContext>(p => p.UseSqlServer(connectionString));

            services.AddScoped<IAchievementsRepository, AchievementsSqlRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Achievement, AchievementDto>();
                cfg.CreateMap<AchievementDto, Achievement>();
                cfg.CreateMap<AchievementForUpdateDto, Achievement>();
                cfg.CreateMap<Achievement, AchievementForUpdateDto>();
                cfg.CreateMap<AchievementForCreationDto, Achievement>();
            });

            app.UseMvc();

            //app.Run((context) =>
            //{
            //    throw new Exception("Aaaa!");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
