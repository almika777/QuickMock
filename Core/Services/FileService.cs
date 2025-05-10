using System.Text;
using System.Text.RegularExpressions;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Core.Services;

public class FileService(IOptions<AppOptions> options)
{
    public async Task AddFile(string path, string value)
    {
        var pathToSave = GetBase64Path(path);

        await File.WriteAllTextAsync(pathToSave, value);
    }

    public async Task<string> GetRequestValue(string path)
    {
        var pathToRead = GetBase64Path(path);
        return await File.ReadAllTextAsync(pathToRead);
    }

    public List<string> GetRequests()
    {
        var fileNames = Directory.GetFiles(options.Value.FullFolderPath);
        return fileNames.Select(GetFileName).ToList();
    }

    private static string GetFileName(string x)
    {
        return Uri.UnescapeDataString(Path.GetFileNameWithoutExtension(x));
    }

    private string GetBase64Path(string path)
    {
        var pathWithFileName = Path.Combine(
            options.Value.FullFolderPath,
            $"{Uri.EscapeDataString(path)}.txt");

        var directory = Path.GetDirectoryName(pathWithFileName);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory!);

        return pathWithFileName;
    }
    
}