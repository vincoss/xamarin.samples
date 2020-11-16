using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Xamarin_LiteDb_Samples.Interface
{
    public interface IStorageService
    {
        Task<T> GetAsync<T>(string key);
        Task SaveAsync<T>(string key, T obj);
        Task RemoveAsync(string key);
    }
}
