using Core.Exceptions;
using Core.Requests;

namespace Core.Services;

public class RequestProviderCacheService : IRequestProviderService
{
    private readonly IDictionary<string, string> _cache = new Dictionary<string, string>();

    public Task AddRequest(RequestAddRequest request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);

        if (_cache.ContainsKey(key))
            throw new HandledCustomException($"'{key}' already exists. Remove older, or rewrite it.");
        
        _cache.Add(key, request.Value);

        return Task.FromResult(key);
    }

    public Task<string> GetRequestValue(RequestGetRequest request)
    {
        var key = GetKey(request.Path, request.IgnoreQueryString);

        if (_cache.TryGetValue(key, out var result))
            return Task.FromResult(result);

        throw new HandledCustomException($"'{request.Path}' doesn't exists. Please add folder, or check path.");
    }

    public Task<List<string>> GetRequests()
    {
        return Task.FromResult(_cache.Keys.ToList());
    }

    private string GetKey(string path, bool ignoreQuery)
    {
        return ignoreQuery
            ? path.Split("?")[0]
            : path;
    }
}