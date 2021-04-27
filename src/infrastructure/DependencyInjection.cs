using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Infrastructure.Identity;
using LibraryApplication.Infrastructure.Persistence;
using LibraryApplication.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace LibraryApplication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string migrationAssembly = typeof(ApplicationDbContext).Assembly.FullName;

            if (configuration.GetValue<bool>("DbSettings:UseSQLite"))
            {
                var useEncryption = configuration.GetValue<bool>("DbSettings:SQLiteSettings:UseEncryption");
                var connectionString = new SqliteConnectionStringBuilder
                {
                    DataSource = configuration["DbSettings:SQLiteSettings:SQLiteFilePath"],
                    Password = useEncryption ? configuration["DbSettings:SQLiteSettings:SQLitePassword"] : null
                };

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(
                        connectionString.ConnectionString,
                        b => b.MigrationsAssembly(migrationAssembly)));
            }

            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration["DbSettings:SQLServerConnection"],
                        b => b.MigrationsAssembly(migrationAssembly)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.Password = Config.GetPasswordOptions();
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
