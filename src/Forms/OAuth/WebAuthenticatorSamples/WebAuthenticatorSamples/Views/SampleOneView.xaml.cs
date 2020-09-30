using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using WebAuthenticatorSamples.Services;

namespace WebAuthenticatorSamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleOneView : ContentPage
    {
        public SampleOneView()
        {
            InitializeComponent();
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var service = new DropboxAuthService();
                var url = service.CreateAuthorizationRequest(DropboxConfiguration.AuthorityUrl);
                authorizeUrl.Text = url;

                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                         new Uri(url),
                         new Uri(DropboxConfiguration.RedirectUri));

                editorAuthResponse.Text = JsonSerializer.Serialize(authenticationResult);

                var code = authenticationResult.Properties["code"];

                var result = await service.GetTokenAsync(code);
                editorTokenResponse.Text = JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }

        public class DropboxConfiguration
        {
            // Auth
            public const string ClientId = "lofbktt2ah7hu89"; //App key
            public const string AuthorityUrl = "https://www.dropbox.com/oauth2/authorize";
            public const string RedirectUri = "com.companyname.webauthenticatorsamples:/oauth2redirect";
            public const string ResponseType = "code";
            public const string Scope = "account_info.read";

            // Token
            public const string TokenApiUri = "https://api.dropboxapi.com/oauth2/token";
            public const string GrantType = "authorization_code";
        }

        public class DropboxAuthResponseModel
        {
            [JsonPropertyName("uid")]
            public string UId { get; set; }

            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }

            [JsonPropertyName("scope")]
            public string Scope { get; set; }

            [JsonPropertyName("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonPropertyName("account_id")]
            public string AccountId { get; set; }
        }

        public interface IAuthService
        {
            Task OnSignInAsync();
            Task OnSignOutAsync();
        }

        public class DropboxAuthService : IAuthService
        {
            private string _codeVerifier;
            public static int PKCEVerifierLength = 128;

            public Task OnSignInAsync()
            {
                var url = CreateAuthorizationRequest(DropboxConfiguration.AuthorityUrl);

                throw new NotImplementedException();
            }

            public Task OnSignOutAsync()
            {
                throw new NotImplementedException();
            }

            public string CreateAuthorizationRequest(string baseUrl)
            {
                if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));

                // Dictionary with values for the authorize request
                var dic = new Dictionary<string, string>();
                dic.Add("client_id", DropboxConfiguration.ClientId);
                dic.Add("token_access_type", "offline");
                dic.Add("response_type", DropboxConfiguration.ResponseType);
                dic.Add("scope", DropboxConfiguration.Scope);
                dic.Add("redirect_uri", DropboxConfiguration.RedirectUri);

                dic.Add("code_challenge", CreateCodeChallenge());
                dic.Add("code_challenge_method", "S256");

                // Add CSRF token to protect against cross-site request forgery attacks.
                var currentCSRFToken = Guid.NewGuid().ToString("N");
                dic.Add("state", currentCSRFToken);

                var authorizeUri = QueryHelpers.AddQueryString(baseUrl, dic);
                return authorizeUri;
            }

            public async Task<DropboxAuthResponseModel> GetTokenAsync(string code)
            {
                if (string.IsNullOrWhiteSpace(code)) throw new ArgumentNullException(nameof(code));

                var parameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", DropboxConfiguration.GrantType },
                    { "client_id", DropboxConfiguration.ClientId },
                    { "redirect_uri", DropboxConfiguration.RedirectUri },
                    { "code_verifier", _codeVerifier },
                };

                var httpClient = new HttpClient();
                var content = new FormUrlEncodedContent(parameters);
                var response = await httpClient.PostAsync(DropboxConfiguration.TokenApiUri, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<DropboxAuthResponseModel>(json);
                    return result;
                }

                return null;
            }

            private string CreateCodeChallenge()
            {
                _codeVerifier = GeneratePKCECodeVerifier();
                var codeChallenge = GeneratePKCECodeChallenge(_codeVerifier);
                return codeChallenge;
            }

            private string GeneratePKCECodeVerifier()
            {
                var bytes = new byte[PKCEVerifierLength];
                RandomNumberGenerator.Create().GetBytes(bytes);
                return Convert.ToBase64String(bytes)
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Substring(0, 128);
            }

            public static string GeneratePKCECodeChallenge(string codeVerifier)
            {
                using (var sha256 = SHA256.Create())
                {
                    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                    return Convert.ToBase64String(challengeBytes)
                        .TrimEnd('=')
                        .Replace('+', '-')
                        .Replace('/', '_');
                }
            }
        }
    }
}
