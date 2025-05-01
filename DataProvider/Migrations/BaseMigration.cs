using FluentMigrator;
using Npgsql;

namespace DataProvider.Migrations;

public abstract class BaseMigration : Migration
{
    protected abstract void Migration();

    protected bool ColumnExists(string tableName, string columnName)
    {
        var exists = false;
        Execute.WithConnection((connection, transaction) =>
        {
            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM information_schema.columns
                    WHERE table_name = @tableName
                      AND column_name = @columnName
                );";
            command.Parameters.Add(new NpgsqlParameter("tableName", tableName));
            command.Parameters.Add(new NpgsqlParameter("columnName", columnName));
            
            exists = (bool)command.ExecuteScalar()!;
        });
        return exists;
    }
    public override void Up()
    {
        Migration();
    }

    public override void Down()
    {
        
    }
}