using AutoMapper;
using Keopi.Repository;
using Keopi.Repository.Cafes;
using Keopi.Repository.Helpers;
using Keopi.Service.Areas;
using Keopi.Service.Cafes;
using Keopi.Service.Cities;
using Keopi.Service.Events;
using Keopi.Service.Images;
using Keopi.Service.PromoCafes;
using KeopiAPI.AutoMapper;
using KeopiDataAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI
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
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = Configuration.GetSection("DatabaseSettings")
                .Get<DatabaseSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICafesService, CafesService>();
            services.AddScoped<ICafesRepository, CafesRepository>();

            services.AddScoped<IPromoCafesService, PromoCafesService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<IAreasService, AreasService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IImagesService, ImagesService>();

            services.AddScoped<IRepositoryHelper,RepositoryHelper>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KeopiAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KeopiAPI v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
