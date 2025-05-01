using FluentMigrator;

namespace DataProvider.Migrations._2025._04
{
    [Migration(202504290040)]
    public class _202504290040_alter_Tariffs_Duration : BaseMigration
    {
        protected override void Migration()
        {
            Alter.Table("Tariffs")
                .AddColumn("Duration").AsCustom("interval")
                .SetExistingRowsTo(TimeSpan.FromDays(30)).NotNullable();
        }
    }
}