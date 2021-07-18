using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MockSuppliersServerApp.Infrastructure;
using MockSuppliersServerApp.Infrastructure.AppSettings;
using MockSuppliersServerApp.Infrastructure.Authentication;
using MockSuppliersServerApp.Infrastructure.Filters;
using MockSuppliersServerApp.Infrastructure.ModelBinders.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockSuppliersServerApp
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
            services.AddControllers(config => config.ModelBinderProviders.Insert(0, new CustomModelBinderProvider())).AddXmlSerializerFormatters();

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                options.AddPolicy("requireAuthUser", policy);
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilters));
                options.Filters.Add(new AuthorizeFilter("requireAuthUser"));
            });

            services.AddAuthentication("CustomAuthWebAPI")
                    .AddScheme<CustomBasicAuthOptions, CustomBasicAuthHandler>("CustomAuthWebAPI", null);

            services.Configure<MvcOptions>(opts =>
            {
                opts.RespectBrowserAcceptHeader = true;
                opts.ReturnHttpNotAcceptable = true;
            });

            services.AddTransient<IUtility, Utility>();
            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
