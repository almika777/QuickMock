using Core.Requests;

namespace Core.Services;

public class RequestProviderService(IRequestProviderService requestProvider) : IRequestService
{
    public async Task AddRequest(RequestAddRequest request)
    {
        await requestProvider.AddRequest(request);
    }
    
    public async Task<string> GetRequestValue(string path)
    {
        return await requestProvider.GetRequestValue(path);
    }
}

public interface IRequestService
{
    Task AddRequest(RequestAddRequest request);
}