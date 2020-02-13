using SQLite;
using System;
using Xamarin_SqliteCipher;
using Xamarin_SqliteCipher.Models;
using Xunit;
using System.Linq;
using System.IO;


namespace Xamarin_Sqlite.Services
{
    public class SqliteDataStoreCipherTest
    {
        [Fact]
        public async void CipherTest()
        {
            var path = $@"C:\Temp\Sqlite\{nameof(CipherTest)}.db3";
            var service = new SqliteDataStoreCipher(path);
            SQLiteAsyncConnection connection = service.Conn;

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
                File.Delete(path);
            }
        }

        [Fact]
        public async void ChangePasswordTest()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(ChangePasswordTest)}.db3";
            var path = Path.Combine(dir, db);
            var service = new SqliteDataStoreCipher(path);

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var item = new Item { Id = i.ToString(), Text = $"Title {i}", Description = $"This is description {i}" };
                    await service.AddItemAsync(item);
                }

                var results = await service.GetItemsAsync("ti");
                Assert.True(results.Count() > 0, $"Count: {results.Count()}");

                ChangePassword(service.Conn);
                service.Conn = null;
                service.Password = "password1234";

                results = await service.GetItemsAsync("title 3");
                Assert.True(results.Count() > 0, $"Count: {results.Count()}");
            }
            finally
            {
                if (service.Conn != null)
                {
                    await service.Conn.CloseAsync();
                    service.Conn = null;
                }
                File.Delete(path);
            }
        }

        private async void ChangePassword(SQLiteAsyncConnection connection)
        {
            await connection.ExecuteAsync("PRAGMA key = 'password123';");
            await connection.ExecuteAsync("PRAGMA rekey = 'password1234';");
        }

    }
}