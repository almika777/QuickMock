namespace Core.Requests;

public class RequestGetRequest
{
    public string Path { get; set; }
    public bool IgnoreQueryString { get; set; }
}