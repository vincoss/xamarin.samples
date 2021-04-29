using NetGenericHost.Interfaces;
using System;
using System.Net.Http;


namespace NetGenericHost.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    }
}