using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Samples.Interfaces
{
    public interface IStorageService
    {
        Task<T> GetAsync<T>(string key);
        Task SaveAsync<T>(string key, T obj);
        Task RemoveAsync(string key);
    }
}
