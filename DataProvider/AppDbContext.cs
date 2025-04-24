using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;

namespace DataProvider
{
    internal class AppDbContext : DataConnection
    {
        public AppDbContext(string connectionString)
            : base(PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v15), connectionString)
        {
        }
    }
}
