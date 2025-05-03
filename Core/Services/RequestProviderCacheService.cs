using Core.Exceptions;
using Core.Requests;

namespace Core.Services;

public class RequestProviderCacheService : IRequestProviderService
{
    private readonly IDictionary<string, string> _cache = new Dictionary<string, string>();

    public Task<string> AddRequest(RequestAddRequest request)
    {
        if (_cache.ContainsKey(GetKey(request.Path)))
            throw new HandledCustomException($"'{request.Path}' already exists. Remove older, or rewrite it.");

        _cache.Add(request.Path, request.Value);

        return Task.FromResult(GetKey(request.Path));
    }

    public Task<string> GetRequestValue(string path)
    {
        if (_cache.TryGetValue(GetKey(path), out var result))
            return Task.FromResult(result);

        throw new HandledCustomException($"'{path}' doesn't exists. Please add folder, or check path.");
    }

    private string GetKey(string path) => path;
}