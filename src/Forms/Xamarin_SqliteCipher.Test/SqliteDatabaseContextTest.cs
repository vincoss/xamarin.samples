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
    public class SqliteDatabaseContextTest
    {
        [Fact]
        public async void Test()
        {
            var dir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var db = $"{nameof(SqliteDatabaseContextTest)}.db3";
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
               await service.CloseConnectionAsync();
            }
        }
    }
}
