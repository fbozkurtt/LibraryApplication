using LibraryApplication.Application;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Infrastructure;
using LibraryApplication.Web.API.Filters;
using LibraryApplication.Web.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics;
using System.Reflection;

namespace LibraryApplication.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        public IConfiguration Configuration { get; }
        public string ProductVersion { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Adds the services in LibraryApplication.Application
            services.AddApplication();

            services.AddHttpContextAccessor();

            // Adds the services in LibraryApplication.Infrastructure
            services.AddInfrastructure(Configuration);

            // Adds a service for determining the authenticated user
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            // Adds a cors policy for allowing cross origin requests from the client application
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSPA",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:5555")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            // Adds a custom exception middleware for request pipeline
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());

            // Adds a swagger doc generator service
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ProductVersion, new OpenApiInfo { Title = "Library API", Version = ProductVersion });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }


        // Configures middlewares. Do not change order of the middleware usages otherwise the application will fail in runtime
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSPA");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{ProductVersion}/swagger.json", $"Library API {ProductVersion}");
                options.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
