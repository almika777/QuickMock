using DataProvider.Migrations._2025._04;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataProvider.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataProvider(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("QuickMock")!;
            services.AddScoped<AppDbContext>(x => new AppDbContext(connectionString));

            using var provider = services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(_202504191340_initialize).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

            using var scope = provider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            try
            {
                runner.MigrateUp();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return services;
        }
    }
}
