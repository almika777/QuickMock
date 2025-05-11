namespace Core.Models;

public class RequestModel
{
    public string Path { get; set; }
    public string? Value { get; set; }
    public bool IgnoreQueryString { get; set; }
}