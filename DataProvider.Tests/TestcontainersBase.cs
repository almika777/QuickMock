using DataProvider.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
#pragma warning disable NUnit1032

namespace DataProvider.Tests;

public class TestcontainersBase
{
    protected IServiceProvider Services { get; private set; }
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
                new("ConnectionStrings:QuickMock",ConnectionString)
            }).Build();

        var services = new ServiceCollection();
        services.AddDataProvider(config);

        Services = services.BuildServiceProvider();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}
