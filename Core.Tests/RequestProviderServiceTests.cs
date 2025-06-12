using Common.Options;
using Core.Models;
using Core.Services;
using Microsoft.Extensions.Options;

namespace Core.tests;

public class RequestProviderServiceTests
{
    private readonly AppOptions _options = new AppOptions
    {
        RequestsFolder = "temp"
    };

    private readonly RequestModel _addRequest = new()
    {
        Url = "http://localhost:5000/qwe/dfg?text=123",
        Value = "value",
    };

    private readonly RequestModel _addRequest2 = new()
    {
        Url = "http://localhost:5000/qwe/dfg?text=1234",
        Value = "value2",
    };

    [Test]
    public async Task AddRequest_WhenIgnoreAndNotIgnoreContainsSamePath()
    {
        var service = new RequestProviderService(new FileService(new OptionsWrapper<AppOptions>(_options)));

        await AddTwoRequests(service);

        var directoryExists = Directory.Exists(_options.FullFolderPath);
        var filesInDirectory = Directory.GetFiles(_options.FullFolderPath);

        Assert.That(directoryExists, Is.True);
        Assert.That(filesInDirectory.Length, Is.EqualTo(2));
    }

    [Test]
    public async Task GetRequests_ReturnAllRequests()
    {
        var service = new RequestProviderService(new FileService(new OptionsWrapper<AppOptions>(_options)));

        await AddTwoRequests(service);

        var res = service.GetRequests();

        Assert.That(res.Count, Is.EqualTo(2));
    }
    
    private async Task AddTwoRequests(RequestProviderService service)
    {
        await service.AddRequest(_addRequest);
        await service.AddRequest(_addRequest2);
    }

    [TearDown]
    public void TearDown()
    {
        Directory.Delete(_options.FullFolderPath, true);
    }
}