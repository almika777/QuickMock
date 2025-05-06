namespace QuickMock.Models.Request;

public class RequestAddModel
{
    public string Path { get; set; }
    public string Value { get; set; }
    public bool IgnoreQueryString { get; set; }
}