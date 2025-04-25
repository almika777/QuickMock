using FluentMigrator;

namespace DataProvider.Migrations
{
    [Migration(1)]
    public class _1_all_scheme : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("Username").AsString(128).NotNullable()
                .WithColumn("Password").AsString(128).NotNullable()
                .WithColumn("RefreshToken").AsString(512).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Comment").AsString(256).Nullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

            Create.Table("BigRequests")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("Path").AsString(256).NotNullable()
                .WithColumn("HttpMethod").AsString(32).NotNullable()
                .WithColumn("Value").AsBinary(8192).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

            Create.Table("Tariffs")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Price").AsInt32().NotNullable()
                .WithColumn("RequestsPerMinute").AsInt16().NotNullable()
                .WithColumn("SimpleRequestsCount").AsInt16().NotNullable()
                .WithColumn("BigRequestsCount").AsInt16().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

            Create.Table("SimpleRequests")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("Path").AsString(256).NotNullable()
                .WithColumn("HttpMethod").AsString(32).NotNullable()
                .WithColumn("Value").AsBinary(2048).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

            Create.Table("Subscriptions")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("TariffId").AsGuid().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

            Create.Table("Payments")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("SubscriptionsId").AsGuid().NotNullable()
                .WithColumn("Amount").AsDouble().NotNullable()
                .WithColumn("Status").AsString(32).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

        }

        public override void Down()
        {
            
        }
    }
}
