using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin_SqliteCipher.Data;
using Xunit;

namespace Xamarin_SqliteCipher.Test
{
    public class BaseSqliteDatabaseEngineTest
    {
        [Fact]
        public async void DatabaseExists_False()
        {
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(DatabaseExists_False)}.db3")
            };

            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                var result = context.DatabaseExists();

                Assert.False(result);
            }
            finally
            {
               await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void DatabaseExists()
        {
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(DatabaseExists)}.db3")
            };

            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await context.CreateDatabaseAsync();
                var result = context.DatabaseExists();

                Assert.True(result);
            }
            finally
            {
               await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void TableExistsAsync()
        {
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(TableExistsAsync)}.db3")
            };

            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await context.CreateDatabaseAsync();

                var t = await context.TableExistsAsync(nameof(ProductTable));
                var f = await context.TableExistsAsync("test");

                Assert.True(t);
                Assert.False(f);
            }
            finally
            {
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void ChangeDbKey()
        {
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(TableExistsAsync)}.db3")
            };

            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await context.CreateDatabaseAsync();

                // Create and change the key
                var updateKey = Guid.NewGuid().ToString();
                await context.ChangeDbKeyAsync(config.DatabaseKey, updateKey);

                // close
                await context.CloseConnectionAsync();

                // New context with key
                config.DatabaseKey = updateKey;
                context = new BaseSqliteDatabaseEngineImpl(config);

                // Shall be able to connect
                var t = await context.TableExistsAsync(nameof(ProductTable));

                Assert.True(t);
            }
            finally
            {
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void RestoreAsync_Success()
        {
            var backupConfig = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_Success)}-backup.db3")
            }; 
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_Success)}.db3")

            };

            var backup = new BaseSqliteDatabaseEngineImpl(backupConfig);
            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await backup.CreateDatabaseAsync();
                await backup.CloseConnectionAsync();
                await context.CreateDatabaseAsync();

                var result = await context.RestoreAsync(backupConfig.DatabasePath);

                Assert.True(result.IsSuccessful, result.Exception?.Message);
                Assert.True(result.IsCorrectDatabase);
                Assert.False(result.IsMigrationRequired);
                Assert.Null(result.Exception);
               
            }
            finally
            {
                await backup.DeleteDatabaseAsync();
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void RestoreAsync_FailedNotADatabase()
        {
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_FailedNotADatabase)}.db3")

            };

            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await context.CreateDatabaseAsync();

                var path = Path.Combine(AppContext.BaseDirectory, "Fake.test");
                var result = await context.RestoreAsync(path);

                Assert.False(result.IsSuccessful, result.Exception?.Message);
                Assert.False(result.IsCorrectDatabase);
                Assert.False(result.IsMigrationRequired);
                Assert.NotNull(result.Exception);

            }
            finally
            {
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void RestoreAsync_FailedWrongKey()
        {
            var backupConfig = new SqliteDatabaseConfiguration()
            {
                DatabaseKey = "some-key",
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_FailedWrongKey)}-backup.db3")
            };
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_FailedWrongKey)}.db3")

            };

            var backup = new BaseSqliteDatabaseEngineImpl(backupConfig);
            var context = new BaseSqliteDatabaseEngineImpl(config);

            try
            {
                await backup.CreateDatabaseAsync();
                await backup.CloseConnectionAsync();
                await context.CreateDatabaseAsync();

                var result = await context.RestoreAsync(backupConfig.DatabasePath);

                Assert.False(result.IsSuccessful, result.Exception?.Message);
                Assert.False(result.IsCorrectDatabase);
                Assert.False(result.IsMigrationRequired);
                Assert.NotNull(result.Exception);
            }
            finally
            {
                await backup.DeleteDatabaseAsync();
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void RestoreAsync_FailedWrongDatabase()
        {
            var backupConfig = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_FailedWrongDatabase)}-backup.db3")
            };
            var config = new SqliteDatabaseConfiguration()
            {
                DatabasePath = Path.Combine(AppContext.BaseDirectory, $"{nameof(RestoreAsync_FailedWrongDatabase)}.db3")

            };

            var backup = new BaseSqliteDatabaseEngineImpl(backupConfig);
            var context = new BaseSqliteDatabaseEngineImpl(config);
            context.IsCorrectDatabasePropety = false;
            context.IsUpgradeRequiredProperty = true;

            try
            {
                await backup.CreateDatabaseAsync();
                await backup.CloseConnectionAsync();
                await context.CreateDatabaseAsync();

                var result = await context.RestoreAsync(backupConfig.DatabasePath);

                Assert.False(result.IsSuccessful, result.Exception?.Message);
                Assert.False(result.IsCorrectDatabase);
                Assert.True(result.IsMigrationRequired);
                Assert.Null(result.Exception);
            }
            finally
            {
                await backup.DeleteDatabaseAsync();
                await context.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public void SqliteDatabaseConfigurationTest()
        {
            var config = new SqliteDatabaseConfiguration();

            var ep = Path.Combine(AppContext.BaseDirectory, "Database.db3");
            Assert.Equal("Pass@word1", config.DatabaseKey);
            Assert.Equal(ep, config.DatabasePath);
        }

        class BaseSqliteDatabaseEngineImpl : BaseSqliteDatabaseEngine
        {
            public BaseSqliteDatabaseEngineImpl(SqliteDatabaseConfiguration config) : base(config)
            { }

            public async override Task CreateDatabaseAsync()
            {
                if (await TableExistsAsync(nameof(ProductTable)) == false)
                {
                    await Database.CreateTableAsync<ProductTable>();
                }
            }

            protected override SQLiteOpenFlags GetFlags()
            {
                return SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;
            }

            public bool? IsCorrectDatabasePropety { get; set; }
            protected async override Task<bool> IsCorrectDatabase(SQLiteAsyncConnection connection)
            {
                if(IsCorrectDatabasePropety != null)
                {
                    return IsCorrectDatabasePropety.Value;
                }
                var tableName = nameof(ProductTable);
                var result = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(type) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';");
                return result > 0;
            }

            public bool IsUpgradeRequiredProperty { get; set; }
            protected override Task<bool> IsUpgradeRequired(SQLiteAsyncConnection connection)
            {
                return Task.FromResult(IsUpgradeRequiredProperty);
            }
        }

        public class ProductTable
        {
            [PrimaryKey, NotNull, Indexed, Collation("NOCASE"), MaxLength(32)]
            public string Key { get; set; }
        }
    }
}
