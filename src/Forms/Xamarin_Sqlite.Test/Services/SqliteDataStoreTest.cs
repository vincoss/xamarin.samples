using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Xamarin_Sqlite.Models;
using Xunit;


namespace Xamarin_Sqlite.Services
{
    public class SqliteDataStoreTest
    {
        [Fact]
        public async void Test()
        {
            var service = new SqliteDataStore();
            var path = service.GetDatabasePath();
            SQLiteAsyncConnection connection = service.GetConnection(path);

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var item = new Item { Id = i.ToString(), Text = $"Title {i}", Description = $"This is description {i}" };
                    await service.AddItemAsync(item);
                }
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    connection = null;
                }

                File.Delete(path);
            }
        }

        [Fact]
        public async void QueryTest()
        {
            //var path = @"C:\Temp\Sqlite\sqlitedb.db3";
            var service = new SqliteDataStore();
            var path = service.GetDatabasePath();
            SQLiteAsyncConnection connection = service.GetConnection(path);

            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    var item = new Item { Id = i.ToString(), Text = $"Title {i}", Description = $"This is description {i}" };
                    await service.AddItemAsync(item);
                }

                var results = await service.GetItemsAsync("ti");

                Assert.True(results.Count() > 0, $"Count: {results.Count()}");
            }
            finally
            {
                if (connection != null)
                {
                    await connection.CloseAsync();
                    connection = null;
                }
                File.Delete(path);
            }
        }

    }
}
