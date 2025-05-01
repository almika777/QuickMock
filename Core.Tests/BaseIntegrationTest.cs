using Core.Extensions;
using DataProvider;
using DataProvider.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

#pragma warning disable NUnit1032

namespace Core.tests;

public class BaseIntegrationTest
{
    protected IServiceProvider Services { get; private set; }
    protected AppDbContext Db { get; private set; }
    protected string ConnectionString { get; private set; }
    private PostgreSqlContainer _postgreSqlContainer;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .Build();

        await _postgreSqlContainer.StartAsync();
        ConnectionString = _postgreSqlContainer.GetConnectionString();
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
            {
                new("ConnectionStrings:QuickMock", ConnectionString),
                new("Jwt:Key", "randomstring123123123f342f123f34y34g34f"),
            }).Build();

        var services = new ServiceCollection();
        
        services.AddSingleton<IConfiguration>(config);
        
        services.AddDataProvider(config);
        services.AddCore(config);

        Services = services.BuildServiceProvider();
        Db = Services.GetRequiredService<AppDbContext>();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _postgreSqlContainer.DisposeAsync();
        await Db.DisposeAsync();
    }
}