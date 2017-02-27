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
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddScoped<IAchievementCategoryRepository, AchievementCategorySqlRepository>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Nucleus API", Version = "v1" });
            });
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
                //cfg.CreateMap<AchievementDto, Achievement>();
                cfg.CreateMap<AchievementForCreationDto, Achievement>();
                cfg.CreateMap<AchievementForUpdateDto, Achievement>();
                cfg.CreateMap<Achievement, AchievementForUpdateDto>();

                cfg.CreateMap<AchievementCategory, AchievementCategoryDto>();
                cfg.CreateMap<AchievementCategoryForCreationDto, AchievementCategory>();
                cfg.CreateMap<AchievementCategoryForUpdateDto, AchievementCategory>();
                cfg.CreateMap<AchievementCategory, AchievementCategoryForUpdateDto>();
            });

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
