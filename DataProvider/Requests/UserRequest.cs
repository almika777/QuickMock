namespace DataProvider.Requests;

public class UserRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public bool IsActive { get; set; }
    public string Comment { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}