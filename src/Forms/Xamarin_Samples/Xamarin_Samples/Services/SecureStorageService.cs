using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin_Samples.Interfaces;


namespace Xamarin_Samples.Services
{
    public class SecureStorageService : IStorageService
    {
        private readonly string _keyFormat = "appNameSecureStorage:{0}";
        private readonly JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var formattedKey = string.Format(_keyFormat, key);
            var val = await Xamarin.Essentials.SecureStorage.GetAsync(formattedKey);
            if (typeof(T) == typeof(string))
            {
                return (T)(object)val;
            }
            else
            {
                return JsonSerializer.Deserialize<T>(val, _serializeOptions);
            }
        }

        public async Task SaveAsync<T>(string key, T obj)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            if (obj == null)
            {
                await RemoveAsync(key);
                return;
            }
            var formattedKey = string.Format(_keyFormat, key);
            if (typeof(T) == typeof(string))
            {
                await Xamarin.Essentials.SecureStorage.SetAsync(formattedKey, obj as string);
            }
            else
            {
                await Xamarin.Essentials.SecureStorage.SetAsync(formattedKey, JsonSerializer.Serialize(obj, _serializeOptions));
            }
        }

        public Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var formattedKey = string.Format(_keyFormat, key);
            Xamarin.Essentials.SecureStorage.Remove(formattedKey);
            return Task.FromResult(0);
        }
    }
}
