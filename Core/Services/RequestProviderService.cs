using Core.Models;

namespace Core.Services;

public class RequestProviderService(FileService fileService) : IRequestProviderService
{
    public async Task AddRequest(RequestModel request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);
        await fileService.AddFile(key, request.Value);
    }

    public async Task<RequestModel> EditRequest(RequestModel request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);
        await fileService.EditFile(key, request.Value);
        
        return new RequestModel
        {
            Path = request.Path,
            Value = request.Value
        };
    }

    public async Task Delete(string path)
    {
        var key = GetKey(path, false);
        await fileService.DeleteFile(key);
    }

    public async Task<RequestModel> GetRequestValue(string path)
    {
        var res = await fileService.GetRequestValue(GetKey(path, false));
        return new RequestModel
        {
            Value = res ?? await fileService.GetRequestValue(GetKey(path, true))
        };
    }

    public Task<List<string>> GetRequests()
    {
        return Task.FromResult(fileService.GetRequests());
    }

    private string GetKey(string path, bool ignoreQuery)
    {
        return ignoreQuery
            ? path.Split("?")[0]
            : path;
    }
}

public interface IRequestProviderService
{
    Task AddRequest(RequestModel request);
    Task<RequestModel> EditRequest(RequestModel request);
    Task Delete(string path);
    Task<RequestModel> GetRequestValue(string path);
    Task<List<string>> GetRequests();
}