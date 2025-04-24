using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504230012)]
    public class _202504230012_payments : Migration
    {
        private string TableName => "Payments";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("SubscriptionsId").AsGuid().ForeignKey("Subscriptions", "Id").NotNullable()
                .WithColumn("Amount").AsDouble().NotNullable()
                .WithColumn("Status").AsString(32).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}
