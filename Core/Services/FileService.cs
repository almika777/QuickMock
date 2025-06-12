using System.IO.Compression;
using System.Text;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Core.Services;

public class FileService(IOptions<AppOptions> options)
{
    private readonly string _folderPath = options.Value?.FullFolderPath ?? Environment.CurrentDirectory;
    public async Task AddFile(string path, string value)
    {
        var pathWithFileName = PathWithFileName(path);
        var directory = Path.GetDirectoryName(pathWithFileName);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory!);

        await File.WriteAllTextAsync(pathWithFileName, value);
    }

    public async Task EditFile(string path, string value)
    {
        var pathWithFileName = PathWithFileName(path);
        await File.WriteAllTextAsync(pathWithFileName, value);
    }

    public void DeleteFile(string path)
    {
        var pathWithFileName = PathWithFileName(path);
        File.Delete(pathWithFileName);
    }

    public async Task<string> GetRequestValue(string path)
    {
        var pathToRead = PathWithFileName(path);
        try
        {
            return await File.ReadAllTextAsync(pathToRead);
        }
        catch (FileNotFoundException)
        {
            return string.Empty;
        }
    }

    public List<string> GetRequests()
    {
        var fileNames = Directory.GetFiles(_folderPath, "*.txt");
        return fileNames.Select(GetDecodedFileName).ToList();
    }

    private string PathWithFileName(string path)
    {
        return Path.Combine(_folderPath, $"{EncodePath(path)}.txt");
    }

    private string GetDecodedFileName(string fileName) 
        => DecodePath(Path.GetFileNameWithoutExtension(fileName));
    
    private string EncodePath(string path)
    {
        var raw = Encoding.UTF8.GetBytes(path);
        using var outputStream = new MemoryStream();
        using (var gzip = new GZipStream(outputStream, CompressionLevel.Optimal))
        {
            gzip.Write(raw, 0, raw.Length);
        }

        return Convert.ToBase64String(outputStream.ToArray())
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private string DecodePath(string filename)
    {
        var decodedBase64 = filename
            .Replace('-', '+')
            .Replace('_', '/');
        var compressed = Convert.FromBase64String(decodedBase64);
        using var inputStream = new MemoryStream(compressed);
        using var gzip = new GZipStream(inputStream, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        gzip.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }
}