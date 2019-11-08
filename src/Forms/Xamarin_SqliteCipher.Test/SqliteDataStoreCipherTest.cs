using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_SqliteCipher;
using Xamarin_SqliteCipher.Models;
using Xunit;
using System.Linq;


namespace Xamarin_Sqlite.Services
{
    public class SqliteDataStoreCipherTest
    {
        [Fact]
        public async void CipherTest()
        {
            var path = @"C:\Temp\Sqlite\sqlitedbcipher.db3";
            var service = new SqliteDataStoreCipher(path);
            //var path = service.GetDatabasePath();
            SQLiteAsyncConnection connection = service.GetConnection(path);

            try
            {
                for (int i = 0; i < 10; i++)
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
                // File.Delete(path);
            }
        }
    }
}
