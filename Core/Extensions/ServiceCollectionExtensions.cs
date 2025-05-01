using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            return services;
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }

    }
}