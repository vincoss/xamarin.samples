using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace WebAuthenticatorSamples.Services
{
    public interface IRequestProvider
    {
        Task<string> GetAsync(string uri, string token = "");
        Task<TResult> PostAsync<TResult>(string uri, string data, string token);
    }
}
