using BaseTest;
using Core.Requests;
using Core.Services;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;

namespace Core.tests;

public class AuthServiceTests : BaseIntegrationTest
{
    private IAuthService _service;

    [SetUp]
    public void Setup()
    {
        _service = Services.GetRequiredService<IAuthService>();
    }

    [Test]
    public async Task RegisterTest()
    {
        var email = "email";
        var password = "password";
        await _service.Register(new RegistrationRequest(email, password));

        var user = await Db.Users.FirstOrDefaultAsync(x => x.Email == email);
        var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == user!.Id);
        var userEmailVerify = await Db.UserEmailVerify.FirstOrDefaultAsync(x => x.UserId == user!.Id);
        
        Assert2.NotNull(user);
        Assert2.NotNull(subscription);
        Assert2.NotNull(userEmailVerify);
    }
}