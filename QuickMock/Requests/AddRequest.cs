namespace QuickMock.Requests;

public record AddRequest(string Path, string Password, bool IgnoreQueryString)
{
    
}