using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace SqliteDapperSample.Database
{
    public class DatabaseConfig
    {
        public static string DbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public DatabaseConfig()
        {
            Name = "sqlitedb.db3";
            DatabasePath = DbPath;
            ConnectionString = $"Data Source={Path.Combine(DatabasePath, Name)};Password=Pass@word1;";
        }

        public string Name { get; private set; }

        public string DatabasePath { get; private set; }

        public string ConnectionString { get; private set; }
    }
}
