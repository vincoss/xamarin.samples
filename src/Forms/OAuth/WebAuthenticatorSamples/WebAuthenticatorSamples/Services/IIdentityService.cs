using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAuthenticatorSamples.Models;


namespace WebAuthenticatorSamples.Services
{
    public interface IIdentityService
    {
        string CreateAuthorizationRequest();
        Task<UserToken> GetTokenAsync(string code);
        Task<UserToken> GetRefreshTokenAsync(string currentRefreshToken);
        Task<string> GetAsync(string uri, string accessToken);
    }
}
