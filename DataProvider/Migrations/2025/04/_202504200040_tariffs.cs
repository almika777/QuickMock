using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504200040)]
    public class _202504200040_tariffs : Migration
    {
        private string TableName => "Tariffs";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("RequestsPerMinute").AsInt16().NotNullable()
                .WithColumn("SimpleRequestsCount").AsInt16().NotNullable()
                .WithColumn("BigRequestsCount").AsInt16().NotNullable()
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
