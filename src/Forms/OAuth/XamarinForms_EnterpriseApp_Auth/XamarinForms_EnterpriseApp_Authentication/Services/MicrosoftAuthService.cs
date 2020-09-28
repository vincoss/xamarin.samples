using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using XamarinForms_EnterpriseApp_Authentication.Models;

namespace XamarinForms_EnterpriseApp_Authentication.Services
{
    public interface IMicrosoftAuthService
    {
        void Initialize();
        Task<User> OnSignInAsync();
        Task OnSignOutAsync();
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-net-initializing-client-applications
    /// https://docs.microsoft.com/en-us/graph/api/user-get?view=graph-rest-1.0&tabs=http
    /// </summary>
    public class MicrosoftAuthService : IMicrosoftAuthService
    {
        private static readonly string ClientID = "TODO: appId";
        private static readonly string[] Scopes = { "User.Read" };
        private static readonly string GraphUrl = "https://graph.microsoft.com/v1.0/me";

        private readonly string RedirectUrl = $"msal{ClientID}://auth";
        //private readonly string RedirectUrl = "com.companyname.webauthenticatorsamples://oauth2redirect";

        private IPublicClientApplication _publicClientApplication;

        public void Initialize()
        {
            this._publicClientApplication = PublicClientApplicationBuilder.Create(ClientID)
                .WithIosKeychainSecurityGroup("com.microsoft.adalcache") // iOS only to access Keychain of the device
                .WithRedirectUri(RedirectUrl)
                .Build();
        }

        /// <summary>
        /// This object is used to know where to display the authentication activity (for Android) or page.
        /// </summary>
        public static object ParentWindow { get; set; }

        /// <summary>
        /// Signin with your Microsoft account.
        /// </summary>
        public async Task<User> OnSignInAsync()
        {
            User currentUser = null;

            var accounts = await this._publicClientApplication.GetAccountsAsync();
            try
            {
                try
                {
                    var firstAccount = accounts.FirstOrDefault();
                    var authResult = await this._publicClientApplication.AcquireTokenSilent(Scopes, firstAccount).ExecuteAsync();
                    currentUser = await this.RefreshUserDataAsync(authResult?.AccessToken).ConfigureAwait(false);
                }
                catch (MsalUiRequiredException ex)
                {
                    // the user was not already connected.
                    try
                    {
                        var authResult = await this._publicClientApplication.AcquireTokenInteractive(Scopes)
                                                    .WithParentActivityOrWindow(ParentWindow)
                                                    .ExecuteAsync();
                        currentUser = await this.RefreshUserDataAsync(authResult?.AccessToken).ConfigureAwait(false);
                    }
                    catch (Exception ex2)
                    {
                        // Manage the exception with a logger as you need
                        System.Diagnostics.Debug.WriteLine(ex2.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Manage the exception with a logger as you need
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return currentUser;
        }

        /// <summary>
        /// Sign out with your Microsoft account.
        /// </summary>
        public async Task OnSignOutAsync()
        {
            var accounts = await this._publicClientApplication.GetAccountsAsync();
            try
            {
                while (accounts.Any())
                {
                    await this._publicClientApplication.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await this._publicClientApplication.GetAccountsAsync();
                }
            }
            catch (Exception ex)
            {
                // Manage the exception with a logger as you need
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Refresh user date from the Graph api.
        /// </summary>
        /// <param name="token">The user access token.</param>
        /// <returns>The current user with his associated informations.</returns>
        private async Task<User> RefreshUserDataAsync(string token)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, GraphUrl);
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            User currentUser = null;

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                currentUser = JsonConvert.DeserializeObject<User>(json);
            }

            return currentUser;
        }
    }
}
