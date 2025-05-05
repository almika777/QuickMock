using Core.Exceptions;
using Core.Requests;
using Core.Services;

namespace Core.tests;

public class RequestProviderCacheServiceTests
{
    [Test]
    public async Task AddRequest_WhenIgnoreAndNotIgnoreContainsSamePath()
    {
        var service = new RequestProviderCacheService();
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
    }

    [Test]
    public async Task AddRequest_WhenTwoIgnoreContainsSamePath()
    {
        var service = new RequestProviderCacheService();
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
            IgnoreQueryString = true
        };

        await service.AddRequest(addRequest);
        Assert.ThrowsAsync<HandledCustomException>(() => service.AddRequest(addRequest2));
    }

    [Test]
    public async Task GetRequest_IgnoreQueryTest()
    {
        var service = new RequestProviderCacheService();

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

        var res1 = await service.GetRequestValue(new RequestGetRequest
        {
            Path = addRequest2.Path,
            IgnoreQueryString = true
        });

        var res2 = await service.GetRequestValue(new RequestGetRequest
        {
            Path = addRequest2.Path,
            IgnoreQueryString = false
        });

        Assert.That(res1, Is.EquivalentTo(addRequest.Value));
        Assert.That(res2, Is.EquivalentTo(addRequest2.Value));
    }
}