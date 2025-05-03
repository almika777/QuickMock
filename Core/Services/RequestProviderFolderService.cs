using System.Text;
using Common.Options;
using Core.Exceptions;
using Core.Requests;
using Microsoft.Extensions.Options;

namespace Core.Services;

public class RequestProviderFolderService(IOptions<AppOptions> options) : IRequestProviderService
{
    private readonly AppOptions _options = options.Value;
    
    public async Task<string> AddRequest(RequestAddRequest request)
    {
        var path = Path.Combine(_options.RequestsFolder, GetCorrectFolderPath(request.Path));
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fullFilePath = Path.Combine(path, $"{Guid.CreateVersion7().ToString()}.txt");
        await File.WriteAllTextAsync(fullFilePath, request.Value);
        return fullFilePath;
    }

    private string GetCorrectFolderPath(string requestPath)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(requestPath));
    }
    public Task<string> GetRequestValue(string path)
    {

        throw new HandledCustomException($"'{path}' doesn't exists. Please add folder, or check path.");
    }

    private string GetKey(string path) => path;
}