using Common.Options;
using Core.Requests;
using Core.Services;
using Microsoft.Extensions.Options;

namespace Core.tests;

public class RequestProviderCacheServiceTests
{
    private readonly AppOptions _options = new AppOptions
    {
        RequestsFolder = "temp"
    };
    
    [Test]
    public async Task AddRequest_WhenIgnoreAndNotIgnoreContainsSamePath()
    {

        var service = new RequestProviderCacheService(new FileService(new OptionsWrapper<AppOptions>(_options)));
        
        var addRequest = new RequestAddRequest
        {
            Path = "qwe/dfg?text=123",
            Value = "value",
            IgnoreQueryString = true
        };

        var addRequest2 = new RequestAddRequest
        {
            Path = "qwe/dfg?text=1563",
            Value = "value2",
            IgnoreQueryString = false
        };

        await service.AddRequest(addRequest);
        await service.AddRequest(addRequest2);

        var directoryExists = Directory.Exists(_options.FullFolderPath);
        var filesInDirectory = Directory.GetFiles(_options.FullFolderPath);
        
        Assert.That(directoryExists, Is.True);
        Assert.That(filesInDirectory.Length, Is.EqualTo(2));
    }

    [Test]
    public async Task GetRequests_ReturnAllRequests()
    {
        var service = new RequestProviderCacheService(new FileService(new OptionsWrapper<AppOptions>(_options)));
        
        var addRequest = new RequestAddRequest
        {
            Path = "http://localhost:5000/qwe/dfg?text=123",
            Value = "value",
            IgnoreQueryString = true
        };

        var addRequest2 = new RequestAddRequest
        {
            Path = "http://localhost:5000/qwe/dfg?text=1563",
            Value = "value2",
            IgnoreQueryString = false
        };

        await service.AddRequest(addRequest);
        await service.AddRequest(addRequest2);

        var res = await service.GetRequests();

        Assert.That(res.Count, Is.EqualTo(2));
    }
    
    [TearDown]
    public void TearDown()
    {
        Directory.Delete(_options.FullFolderPath, true);
    }
}