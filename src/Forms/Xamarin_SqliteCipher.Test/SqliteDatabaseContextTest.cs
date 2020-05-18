using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin_SqliteCipher.Models;
using Xamarin_SqliteCipher.Test.Services;
using Xunit;

namespace Xamarin_SqliteCipher.Test
{

    /// <summary>
    /// Add test cases
    /// wrong password (SQLite.SQLiteException : file is not a database)
    /// </summary>
    public class SqliteDatabaseContextTest
    {
        [Fact]
        public async void ConnectionWithKeyTest()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(ConnectionWithKeyTest)}.db3";
            var path = Path.Combine(dir, db);
            var configuration = new DatabaseConfiguration
            {
                DatabasePath = path,
                DatabaseKey = "test-key"
            };
            var service = new SqliteDatabaseContext(configuration);

            try
            {
                await service.CreateDatabaseAsync();

                for (int i = 0; i < 10; i++)
                {
                    var item = new Item { Id = i.ToString(), Text = $"Title {i}", Description = $"This is description {i}" };
                    await service.Database.InsertAsync(item);
                }

                var result = await service.Database.FindAsync<Item>(x => x.Id == "2");

                Assert.NotNull(result);
            }
            finally
            {
                await service.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void ConnectionWithWrongKeyTest()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(ConnectionWithWrongKeyTest)}.db3";
            var path = Path.Combine(dir, db);
            var configuration = new DatabaseConfiguration
            {
                DatabasePath = path,
                DatabaseKey = "test-key"
            };
            var service = new SqliteDatabaseContext(configuration);

            var configuration1 = new DatabaseConfiguration
            {
                DatabasePath = path,
                DatabaseKey = "asd"
            };
            var service1 = new SqliteDatabaseContext(configuration1);

            try
            {
                await service.CreateDatabaseAsync();

                for (int i = 0; i < 10; i++)
                {
                    var item = new Item { Id = i.ToString(), Text = $"Title {i}", Description = $"This is description {i}" };
                    await service.Database.InsertAsync(item);
                }

                await service.CloseConnectionAsync();

                // SQLite.SQLiteException : file is not a database
                await Assert.ThrowsAsync<SQLiteException>(async () =>
                {
                    await service1.Database.FindAsync<Item>(x => x.Id == "2");
                });
            }
            finally
            {
                await service1.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void GetVersionTest()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(GetVersionTest)}.db3";
            var path = Path.Combine(dir, db);
            var configuration = new DatabaseConfiguration
            {
                DatabasePath = path,
                DatabaseKey = "test-key"
            };
            var service = new SqliteDatabaseContext(configuration);

            try
            {
                await service.CreateDatabaseAsync();
                var sql = "SELECT SQLITE_VERSION()";

                var result = await service.Database.ExecuteScalarAsync<string>(sql);

                Assert.Equal("3.20.1", result);
            }
            finally
            {
                await service.DeleteDatabaseAsync();
            }
        }

        [Fact]
        public async void DatabaseNotCreated()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(DatabaseNotCreated)}.db3";
            var path = Path.Combine(dir, db);
            var configuration = new DatabaseConfiguration
            {
                DatabasePath = path,
                DatabaseKey = "test-key"
            };
            var service = new SqliteDatabaseContext(configuration);

            try
            {
                // SQLite.SQLiteException : no such table: Item
                await Assert.ThrowsAsync<SQLiteException>(async () =>
                {
                    await service.Database.FindAsync<Item>(x => x.Id == "2");
                });
            }
            finally
            {
                await service.DeleteDatabaseAsync();
            }
        }

        //public async Task<bool> IsDbExists(string fileName)
        //{
        //    try
        //    {
        //        var item = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        //        var db = new SQLiteConnection(DbHelper.DBPATH);
        //        var tb1 = db.GetTableInfo("Domain");
        //        var tb2 = db.GetTableInfo("Account");
        //        var tb3 = db.GetTableInfo("Product");
        //        var tb4 = db.GetTableInfo("Review");
        //        if (item == null || tb1.Count == 0 || tb2.Count == 0 || tb3.Count == 0 || tb4.Count == 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
