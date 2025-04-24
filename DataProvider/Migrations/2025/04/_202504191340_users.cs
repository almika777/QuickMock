using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504191340)]
    public class _202504191340_users : Migration
    {
        private string TableName => "Users";
        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Email").AsString(256).NotNullable()
                .WithColumn("Username").AsString(128).NotNullable()
                .WithColumn("Password").AsString(128).NotNullable()
                .WithColumn("RefreshToken").AsString(512).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Comment").AsString(256).NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();

        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}
