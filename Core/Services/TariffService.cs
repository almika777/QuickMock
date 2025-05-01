using DataProvider;
using DataProvider.Entities;
using LinqToDB;

namespace Core.Services;

public class TariffService(AppDbContext db) : ITariffService
{
    public async Task<TariffEntity> GetDefaultTariff()
    {
        return await db.Tariffs.FirstAsync(x => x.Price == 0);
    } 
}

public interface ITariffService
{
    Task<TariffEntity> GetDefaultTariff();
}