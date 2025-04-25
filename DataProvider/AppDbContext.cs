using DataProvider.Entities;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;

namespace DataProvider
{
    public class AppDbContext : DataConnection
    {
        public AppDbContext(string connectionString)
            : base(PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v15), connectionString)
        {
        }

        public ITable<UserEntity> Users => this.GetTable<UserEntity>();
    }
}
