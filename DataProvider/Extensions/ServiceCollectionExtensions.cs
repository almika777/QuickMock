using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataProvider.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataProvider(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
