using DataProvider.Entities;
using DataProvider.Requests;
using LinqToDB;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace DataProvider.Tests.Mappings
{
    public class UserEmailVerifyMappingTests : TestcontainersBase
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
            var entity = new UserEmailVerifyEntity
            {
                Id = Guid.CreateVersion7(),
                VerifyCode = "123456",
                Attempt = 0,
                Created = DateTime.UtcNow
            };
            
            await _db.InsertAsync(entity);

            var res = await _db.UserEmailVerify.FirstAsync(x => x.Id == entity.Id);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.EqualTo(entity.Id));
        }
    }
}
