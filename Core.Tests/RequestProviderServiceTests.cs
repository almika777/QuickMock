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
        Path = "http://localhost:5000/qwe/dfg?text=123",
        Value = "value",
        IgnoreQueryString = true
    };

    private readonly RequestModel _addRequest2 = new()
    {
        Path = "http://localhost:5000/qwe/dfg?text=123",
        Value = "value2",
        IgnoreQueryString = false
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

        var res = await service.GetRequests();

        Assert.That(res.Count, Is.EqualTo(2));
    }


    [Test]
    public async Task GetRequestValue_ReturnCorrectValue()
    {
        var service = new RequestProviderService(new FileService(new OptionsWrapper<AppOptions>(_options)));

        await AddTwoRequests(service);

        var res = await service.GetRequestValue(_addRequest.Path);

        Assert.That(res, Is.EqualTo(_addRequest2.Value));
        //Потому что пути одинаковы, но в 1 реквесте игнорим квери стринг, а пути одинаковы, но во втором ерквесте не игнорим.
        //Было создано 2 файла с одинаковой базой урла, но в 1 случае игнор квери, а в другйо нет.
        //Мы ищем изначально по полному пути, если не нашли, ищем по базовому
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