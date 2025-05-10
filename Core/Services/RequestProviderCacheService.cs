using Core.Exceptions;
using Core.Requests;

namespace Core.Services;

public class RequestProviderCacheService(FileService fileService) : IRequestProviderService
{

    public async Task AddRequest(RequestAddRequest request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);
        await fileService.AddFile(key, request.Value);
    }

    public Task<string> GetRequestValue(RequestGetRequest request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);
        return null;
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