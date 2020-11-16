using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin_LiteDb_Samples.Interface;


namespace Xamarin_LiteDb_Samples.Services
{
    public class LiteDbStorageService : IStorageService
    {
        private static LiteDatabase _db;
        private static readonly object _lock = new object();

        private readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };


        private readonly string _dbPath;
        private ILiteCollection<JsonItem> _collection;
        private Task _initTask;

        public LiteDbStorageService(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath)) throw new ArgumentNullException(nameof(dbPath));

            _dbPath = dbPath;
        }

        public Task InitAsync()
        {
            if (_collection != null)
            {
                return Task.FromResult(0);
            }
            if (_initTask != null)
            {
                return _initTask;
            }
            _initTask = Task.Run(() =>
            {
                try
                {
                    lock (_lock)
                    {
                        if (_db == null)
                        {
                            _db = new LiteDatabase($"Filename={_dbPath};Upgrade=true;");
                        }
                    }
                    _collection = _db.GetCollection<JsonItem>("json_items");
                }
                finally
                {
                    _initTask = null;
                }
            });
            return _initTask;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            await InitAsync();
            var item = _collection.Find(i => i.Id == key).FirstOrDefault();
            if (item == null)
            {
                return default(T);
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(item.Value, _serializeOptions);
        }

        public async Task SaveAsync<T>(string key, T obj)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            await InitAsync();
            var data = System.Text.Json.JsonSerializer.Serialize<T>(obj, _serializeOptions);
            _collection.Upsert(new JsonItem(key, data));
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            await InitAsync();
            _collection.DeleteMany(i => i.Id == key);
        }

        private class JsonItem
        {
            public JsonItem() { }

            public JsonItem(string key, string value)
            {
                if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

                Id = key;
                Value = value;
            }

            public string Id { get; set; }
            public string Value { get; set; }
        }
    }
}
