using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin_SqliteCipher.Models;
using System.Linq;


namespace Xamarin_SqliteCipher.Test.Services
{
    public class DatabaseConfiguration
    {
        public string DatabasePath { get; set; }
        public string DatabaseKey { get; set; } = BaseSqliteDatabaseEngine.DefaultKey;
    }

    public class SqliteDatabaseContext : BaseSqliteDatabaseEngine
    {
        private const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        private readonly DatabaseConfiguration _configuration;

        public SqliteDatabaseContext(DatabaseConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public async override Task CreateDatabaseAsync()
        {
            if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Item).Name))
            {
                await Database.CreateTableAsync<Item>();
            }
        }

        public override string GetDatabaseKey()
        {
          return _configuration.DatabaseKey;
        }

        public override string GetDatabasePath()
        {
            return _configuration.DatabasePath;
        }

        public override SQLiteOpenFlags GetFlags()
        {
            return Flags;
        }
    }
}
