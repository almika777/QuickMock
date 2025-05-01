using Core.Requests;
using DataProvider;
using DataProvider.Entities;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class AuthService(
        AppDbContext db,
        TokenService tokenService,
        ITariffService tariffService,
        ILogger<AuthService> logger) : IAuthService
    {
        private static readonly Random Random = new();
        public async Task Register(RegistrationRequest request)
        {
            var refreshToken = tokenService.GenerateRefreshToken();
            var user = GetUser(request, refreshToken);
            var defaultTariff = await tariffService.GetDefaultTariff();
            var userEmailVerify = GetUserEmailVerify(user.Id);
            var subscription = GetSubscriptions(user.Id, defaultTariff);

            await using var trans = await db.BeginTransactionAsync();
            try
            {
                await db.InsertAsync(user);
                await db.InsertAsync(userEmailVerify);
                await db.InsertAsync(subscription);
                
                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }

        private static UserEntity GetUser(RegistrationRequest request, string refreshToken) => new()
        {
            Id = Guid.CreateVersion7(),
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            RefreshToken = refreshToken,
            IsActive = true,
            Comment = "Первоначальная регистрация",
            Created = DateTime.UtcNow
        };
        
        private static UserEmailVerifyEntity GetUserEmailVerify(Guid userId) => new()
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Attempt = 0,
            VerifyCode = Random.Next(100000, 999999).ToString(),
            Created = DateTime.UtcNow
        };        
        
        private static SubscriptionEntity GetSubscriptions(Guid userId, TariffEntity tariff) => new()
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            TariffId = tariff.Id,
            IsActive = false,
            Deadline = DateTime.UtcNow.Add(tariff.Duration),
            Created = DateTime.UtcNow
        };
    }

    public interface IAuthService
    {
        Task Register(RegistrationRequest request);
    }
}