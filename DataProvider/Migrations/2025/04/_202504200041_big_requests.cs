using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504200041)]
    public class _202504200041_big_requests : Migration
    {
        private string TableName => "BigRequests";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().ForeignKey("Users", "Id").NotNullable()
                .WithColumn("Path").AsString(256).NotNullable()
                .WithColumn("HttpMethod").AsString(32).NotNullable()
                .WithColumn("Value").AsBinary(8192).NotNullable()
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
