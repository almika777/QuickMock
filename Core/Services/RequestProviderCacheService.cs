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

    public async Task<string?> GetRequestValue(string path)
    {
        var res = await fileService.GetRequestValue(GetKey(path, false));
        return res ?? await fileService.GetRequestValue(GetKey(path, true));
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