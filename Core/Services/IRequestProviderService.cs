using Core.Requests;

namespace Core.Services;

public interface IRequestProviderService
{
    Task AddRequest(RequestAddRequest request);
    Task<string?> GetRequestValue(string path);
    Task<List<string>> GetRequests();
}