using System.IO.Compression;
using System.Text;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Core.Services;

public class FileService(IOptions<AppOptions> options)
{
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

    public async Task DeleteFile(string path)
    {
        var pathWithFileName = PathWithFileName(path);
        File.Delete(pathWithFileName);
    }

    public async Task<string?> GetRequestValue(string path)
    {
        var pathToRead = Path.Combine(
            options.Value.FullFolderPath,
            $"{EncodePath(path)}.txt");
        try
        {
            return await File.ReadAllTextAsync(pathToRead);
        }
        // ReSharper disable once RedundantCatchClause
        catch (FileNotFoundException)
        {
            return null;
        }
    }

    public List<string> GetRequests()
    {
        var fileNames = Directory.GetFiles(options.Value.FullFolderPath);
        return fileNames.Select(GetFileName).ToList();
    }

    private string PathWithFileName(string path)
    {
        return Path.Combine(
            options.Value.FullFolderPath,
            $"{EncodePath(path)}.txt");
    }

    private string GetFileName(string x)
    {
        return DecodePath(Path.GetFileNameWithoutExtension(x));
    }

    private string EncodePath(string path)
    {
        var raw = Encoding.UTF8.GetBytes(path);
        using var outputStream = new MemoryStream();
        using (var gzip = new GZipStream(outputStream, CompressionLevel.Optimal))
        {
            gzip.Write(raw, 0, raw.Length);
        }

        return Convert.ToBase64String(outputStream.ToArray()).Replace('+', '-').Replace('/', '_');
    }

    private string DecodePath(string filename)
    {
        var decodedBase64 = filename.Replace('-', '+').Replace('_', '/');
        var compressed = Convert.FromBase64String(decodedBase64);
        using var inputStream = new MemoryStream(compressed);
        using var gzip = new GZipStream(inputStream, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        gzip.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }
}