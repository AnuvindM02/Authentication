using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Infrastructure.Persistence;
using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.Services;
using Authentication.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Authentication.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            //Add DB Context
            string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not set");

            string connectionString = connectionStringTemplate
                .Replace("$SQL_HOST", Environment.GetEnvironmentVariable("SQL_HOST") ?? throw new InvalidOperationException("SQL_HOST is not set"))
                .Replace("$SQL_PORT", Environment.GetEnvironmentVariable("SQL_PORT") ?? throw new InvalidOperationException("SQL_PORT is not set"))
                .Replace("$SQL_DB", Environment.GetEnvironmentVariable("SQL_DB") ?? throw new InvalidOperationException("SQL_DB is not set"))
                .Replace("$SQL_USER", Environment.GetEnvironmentVariable("SQL_USER") ?? throw new InvalidOperationException("SQL_USER is not set"))
                .Replace("$SQL_PASSWORD", Environment.GetEnvironmentVariable("SQL_PASSWORD") ?? throw new InvalidOperationException("SQL_PASSWORD is not set"));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ISigningKeyRepository, SigningKeyRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            //Services
            services.AddScoped<IPasswordHasher, BrcyptPasswordHasher>();

            //Hosted Services
            services.AddHostedService<KeyRotationService>();
            return services;
        }
    }
}
