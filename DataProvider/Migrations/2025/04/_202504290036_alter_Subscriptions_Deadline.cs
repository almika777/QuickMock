using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504290036)]
    public class _202504290036_alter_Subscriptions_Deadline : BaseMigration
    {
        protected override void Migration()
        {
            Alter.Table("Subscriptions")
                .AddColumn("Deadline").AsDateTime().NotNullable();
        }
    }
}