using Core.Requests;

namespace Core.Services;

public class RequestProviderService(IRequestProviderService requestProvider) : IRequestService
{
    public async Task AddRequest(RequestAddRequest request)
    {
        await requestProvider.AddRequest(request);
    }

}

public interface IRequestService
{
    Task AddRequest(RequestAddRequest request);
}