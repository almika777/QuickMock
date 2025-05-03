using Core.Requests;

namespace Core.Services;

public interface IRequestProviderService
{
    Task<string> AddRequest(RequestAddRequest request);
    Task<string> GetRequestValue(string path);
}