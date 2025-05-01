using System.Text;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            AddAuth(services, config);
            return services;
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        private static void AddAuth(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<TokenService>();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                    };
                });
        }
    }
}