using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504271602)]
    public class _202504271602_table_UserEmailVerify : BaseMigration
    {
        protected override void Migration()
        {
            Create.Table("UserEmailVerify")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("VerifyCode").AsString(6).NotNullable()
                .WithColumn("Attempt").AsByte().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable();
        }
    }
}