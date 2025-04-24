using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504230008)]
    public class _202504230008_subscriptions : Migration
    {
        private string TableName => "Tariffs";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Price").AsDouble().NotNullable()
                .WithColumn("RequestsPerMinutes").AsDouble().NotNullable()
                .WithColumn("SimpleRequestsCount").AsInt32().NotNullable()
                .WithColumn("BigRequestsCount").AsInt32().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}
