using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504271620)]
    public class _202504271620_alter_UserIsVerified : BaseMigration
    {
        protected override void Migration()
        {
            Alter.Table("Users")
                .AddColumn("IsVerified").AsBoolean().NotNullable().WithDefaultValue(false);
            
            if (ColumnExists("Users", "Username"))
                Delete.Column("Username").FromTable("Users");
        }
    }
}