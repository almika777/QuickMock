using Core.Models;

namespace Core.Services;

public class RequestProviderService(FileService fileService) : IRequestProviderService
{
    public async Task AddRequest(RequestModel request)
    {
        var key = GetKey(request.Url);
        await fileService.AddFile(key, request.Value);
    }

    public async Task<RequestModel> EditRequest(RequestModel request)
    {
        var key = GetKey(request.Url);
        await fileService.EditFile(key, request.Value);
        
        return new RequestModel
        {
            Url = request.Url,
            Value = request.Value
        };
    }

    public void Delete(string path)
    {
        var key = GetKey(path);
        fileService.DeleteFile(key);
    }

    public async Task<RequestModel> GetRequestValue(string path)
    {
        var res = await fileService.GetRequestValue(GetKey(path));
        return new RequestModel
        {
            Value = res
        };
    }

    public List<string> GetRequests() => fileService.GetRequests();
    private string GetKey(string path) => path;
}

public interface IRequestProviderService
{
    Task AddRequest(RequestModel request);
    Task<RequestModel> EditRequest(RequestModel request);
    void Delete(string path);
    Task<RequestModel> GetRequestValue(string path);
    List<string> GetRequests();
}