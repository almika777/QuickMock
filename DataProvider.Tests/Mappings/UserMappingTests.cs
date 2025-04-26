using DataProvider.Entities;
using DataProvider.Requests;
using LinqToDB;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DataProvider.Tests.Mappings
{
    public class UserMappingTests : TestcontainersBase
    {
        private AppDbContext _db;

        [SetUp]
        public void SetUp()
        {
            _db = Services.GetRequiredService<AppDbContext>();
        }
        
        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }
        
        [Test]
        public async Task GetByIdTests()
        {
            var user = new UserRequest
            {
                Id = Guid.CreateVersion7(),
                Email = "",
                Username = "null",
                Password = "null",
                RefreshToken = "null",
                IsActive = true,
                Created = DateTime.UtcNow,
            };
            
            await _db.InsertAsync(user.Adapt<UserEntity>());

            var res = await _db.Users.FirstAsync(x => x.Id == user.Id);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.EqualTo(user.Id));
        }
    }
}
