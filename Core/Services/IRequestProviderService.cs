using Core.Requests;

namespace Core.Services;

public interface IRequestProviderService
{
    Task AddRequest(RequestAddRequest request);
    Task<string> GetRequestValue(RequestGetRequest request);
    Task<List<string>> GetRequests();
}