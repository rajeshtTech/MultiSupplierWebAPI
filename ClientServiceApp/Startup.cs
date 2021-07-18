using ClientServiceApp.BO;
using ClientServiceApp.Infrastructure.Configuration;
using ClientServiceApp.Infrastructure.Filters;
using ClientServiceApp.Infrastructure.Helper;
using ClientServiceApp.Repositories.WebAPIRepositories;
using ClientServiceApp.Repositories.WebAPIRepositories.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //REPOSITORIES
            services.AddTransient<ISuppXConsignRepository, SuppXConsignRepository>();
            services.AddTransient<ISuppYConsignRepository, SuppYConsignRepository>();
            services.AddTransient<ISuppZConsignRepository, SuppZConsignRepository>();

            //HELPER CLASSES
            services.AddTransient<IHttpHelper, HttpHelper>();
            services.AddTransient<IUtility, Utility>();
            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

            //BO
            services.AddTransient<IConsignmentBO,ConsignmentBO>();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilters));
                options.Filters.Add(typeof(ActionGlobalLogFilter));
            });

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Products API",
                    Description = "Products Web API service"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API"));
        }
    }
}
