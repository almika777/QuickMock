using BaseTest;
using Common.Options;
using Core.Requests;
using Core.Services;
using Microsoft.Extensions.Options;

namespace Core.tests;

public class RequestProviderServiceFolderTests
{
    private static readonly string TestFolder = Path.Combine(Directory.GetCurrentDirectory(), "testFolder");
    
    [Test]
    public async Task AddRequestTest()
    {
        var service = new RequestProviderFolderService(
            new OptionsWrapper<AppOptions>(new AppOptions
            {
                RequestsFolder = TestFolder
            }));

        var res = await service.AddRequest(new RequestAddRequest
        {
            Path = "qwe/dfg?text=123",
            Value = "value"
        });

        Assert2.True(Directory.Exists(TestFolder));
        Assert2.True(File.Exists(res));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Directory.Delete(TestFolder, true);
    }
}