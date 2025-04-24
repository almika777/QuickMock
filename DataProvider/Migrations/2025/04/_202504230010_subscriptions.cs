using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504230010)]
    public class _202504230010_subscriptions : Migration
    {
        private string TableName => "Subscriptions";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("TariffId").AsGuid().ForeignKey("Tariffs","Id").NotNullable()
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
