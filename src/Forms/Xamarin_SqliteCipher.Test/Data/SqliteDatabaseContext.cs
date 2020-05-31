using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin_SqliteCipher.Data;
using Xamarin_SqliteCipher.Models;


namespace Xamarin_SqliteCipher.Data
{
    public class SqliteDatabaseContext : BaseSqliteDatabaseEngine
    {
        private const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public SqliteDatabaseContext(SqliteDatabaseConfiguration configuration) : base(configuration)
        {
        }

        public async override Task CreateDatabaseAsync()
        {
            await Database.CreateTableAsync<Item>();
        }

        protected override SQLiteOpenFlags GetFlags()
        {
            return Flags;
        }

        protected async override Task<bool> IsCorrectDatabase(SQLiteAsyncConnection connection)
        {
            var tableName = nameof(Item);
            var result = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(type) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';");
            return result > 0;
        }

        protected async override Task<bool> IsUpgradeRequired(SQLiteAsyncConnection connection)
        {
            bool upgrade = false;
            if (await IsCorrectDatabase(connection))
            {
                return false; // NOTE: Here check database whether needs upgrade.
            }
            return upgrade;
        }
    }
}