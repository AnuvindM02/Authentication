using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
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
