using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SqliteDapperSample.Database
{
    public interface IDatabaseBootstrap
    {
        void Run();
    }

    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Run()
        {
            using (var connection = new SqliteConnection(databaseConfig.ConnectionString))
            {
                var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
                var tableName = table.FirstOrDefault();

                if (!string.IsNullOrEmpty(tableName) && tableName == "Product")
                {
                    return;
                }

                connection.Execute(@"Create Table Product (
                                     Name VARCHAR(100) NOT NULL,
                                     Description VARCHAR(1000) NULL);");
            }
        }
    }
}
