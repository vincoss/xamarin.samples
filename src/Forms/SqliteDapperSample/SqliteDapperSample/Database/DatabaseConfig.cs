using Microsoft.Data.Sqlite;
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
            //var conString = new SqliteConnectionStringBuilder();
            //conString.DataSource = databaseFilePath;
            //conString.DefaultTimeout = 5000;
            //conString.SyncMode = SynchronizationModes.Off;
            //conString.JournalMode = SQLiteJournalModeEnum.Memory;
            //conString.PageSize = 65536;
            //conString.CacheSize = 16777216;
            //conString.FailIfMissing = false;
            //conString.ReadOnly = false;


            //var connectionString = new SqliteConnectionStringBuilder(baseConnectionString)
            //{
            //    Mode = SqliteOpenMode.ReadWriteCreate,
            //    Password =
            //}.ToString();


            Name = "sqlitedb.db3";
            DatabasePath = DbPath;
            ConnectionString = $"Data Source={Path.Combine(DatabasePath, Name)};Password=Pass@word1;";
        }

        public string Name { get; private set; }

        public string DatabasePath { get; private set; }

        public string ConnectionString { get; private set; }
    }
}
