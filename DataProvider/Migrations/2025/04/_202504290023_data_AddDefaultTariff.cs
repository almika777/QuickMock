using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504290023)]
    public class _202504290023_data_AddDefaultTariff : BaseMigration
    {
        protected override void Migration()
        {
            Insert.IntoTable("Tariffs")
                .Row(new Dictionary<string, object>
                {
                    { "Id", Guid.CreateVersion7() },
                    { "Price", 0 },
                    { "RequestsPerMinute", 10 },
                    { "SimpleRequestsCount", 10 },
                    { "BigRequestsCount", 5 },
                    { "Created", DateTime.UtcNow },
                });
        }
    }
}