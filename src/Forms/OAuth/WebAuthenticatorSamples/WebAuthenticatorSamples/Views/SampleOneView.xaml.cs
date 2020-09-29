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

namespace WebAuthenticatorSamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleOneView : ContentPage
    {
        public SampleOneView()
        {
            InitializeComponent();
        }

        public const string ClientId = "TODO"; //App key
        public const string AuthorityUrl = "https://www.dropbox.com/oauth2/authorize";
        public const string RedirectUri = "com.companyname.webauthenticatorsamples:/oauth2redirect";
        public const string ResponseType = "code";
        public const string Scope = "account_info.read";

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var url = DropboxAuthService.CreateAuthorizationRequest(AuthorityUrl);
                entryUrl.Text = url;

                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                         new Uri(url),
                         new Uri(RedirectUri));

                var accessToken = authenticationResult?.AccessToken;

                if (accessToken == null && authenticationResult.Properties != null && authenticationResult.Properties.ContainsKey("code"))
                {
                    accessToken = authenticationResult.Properties["code"];
                }

                if(authenticationResult != null)
                {
                    var code = authenticationResult.Properties["code"]; ;

                    var result = await DropboxAuthService.RefreshDataAsync(code);

                    lblInfo.Text = result.access_token;
                }

                //if(accessToken != null)
                //{
                //    lblInfo.Text = accessToken;
                //}

            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }

        public interface IAuthService
        {
            Task OnSignInAsync();
            Task OnSignOutAsync();
        }

        public class DropboxAuthService : IAuthService
        {
            private static string _codeVerifier;
            public static int PKCEVerifierLength = 128;

            public Task OnSignInAsync()
            {
                var url = CreateAuthorizationRequest(AuthorityUrl);

                throw new NotImplementedException();
            }

            public Task OnSignOutAsync()
            {
                throw new NotImplementedException();
            }

            public static string CreateAuthorizationRequest(string baseUrl)
            {
                if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));

                // Dictionary with values for the authorize request
                var dic = new Dictionary<string, string>();
                dic.Add("client_id", ClientId);
                dic.Add("token_access_type", "offline");
                dic.Add("response_type", ResponseType);
                dic.Add("scope", Scope);
                dic.Add("redirect_uri", RedirectUri);

                dic.Add("code_challenge", CreateCodeChallenge());
                dic.Add("code_challenge_method", "S256");

                // Add CSRF token to protect against cross-site request forgery attacks.
                var currentCSRFToken = Guid.NewGuid().ToString("N");
                dic.Add("state", currentCSRFToken);

                var authorizeUri = QueryHelpers.AddQueryString(baseUrl, dic);
                return authorizeUri;
            }

            private static string CreateCodeChallenge()
            {
                _codeVerifier = GeneratePKCECodeVerifier();
                var codeChallenge = GeneratePKCECodeChallenge(_codeVerifier);
                return codeChallenge;
            }

            private static string GeneratePKCECodeVerifier()
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

            public static async Task<ResposeInfo> RefreshDataAsync(string code)
            {
                var url = "https://api.dropboxapi.com/oauth2/token";

                var parameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "client_id", SampleOneView.ClientId },
                    { "redirect_uri", SampleOneView.RedirectUri },
                    { "code_verifier", _codeVerifier },
                };

                var httpClient = new HttpClient();
                var content = new FormUrlEncodedContent(parameters);
                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);

                dynamic value = null;

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    value = JsonSerializer.Deserialize<ResposeInfo>(json);
                }

                return value;
            }
        }

        public class ResposeInfo
        {
            public string uid { get; set; }
            public string access_token { get; set; }
        }

        internal static class QueryHelpers
        {
            /// <summary>
            /// Append the given query key and value to the URI.
            /// </summary>
            /// <param name="uri">The base URI.</param>
            /// <param name="name">The name of the query key.</param>
            /// <param name="value">The query value.</param>
            /// <returns>The combined result.</returns>
            public static string AddQueryString(string uri, string name, string value)
            {
                if (uri == null)
                {
                    throw new ArgumentNullException(nameof(uri));
                }

                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                return AddQueryString(
                    uri, new[] { new KeyValuePair<string, string>(name, value) });
            }

            /// <summary>
            /// Append the given query keys and values to the uri.
            /// </summary>
            /// <param name="uri">The base uri.</param>
            /// <param name="queryString">A collection of name value query pairs to append.</param>
            /// <returns>The combined result.</returns>
            public static string AddQueryString(string uri, IDictionary<string, string> queryString)
            {
                if (uri == null)
                {
                    throw new ArgumentNullException(nameof(uri));
                }

                if (queryString == null)
                {
                    throw new ArgumentNullException(nameof(queryString));
                }

                return AddQueryString(uri, (IEnumerable<KeyValuePair<string, string>>)queryString);
            }

            private static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string>> queryString)
            {
                if (uri == null)
                {
                    throw new ArgumentNullException(nameof(uri));
                }

                if (queryString == null)
                {
                    throw new ArgumentNullException(nameof(queryString));
                }

                var anchorIndex = uri.IndexOf('#');
                var uriToBeAppended = uri;
                var anchorText = "";

                // If there is an anchor, then the query string must be inserted before its first occurance.
                if (anchorIndex != -1)
                {
                    anchorText = uri.Substring(anchorIndex);
                    uriToBeAppended = uri.Substring(0, anchorIndex);
                }

                var queryIndex = uriToBeAppended.IndexOf('?');
                var hasQuery = queryIndex != -1;

                var sb = new StringBuilder();
                sb.Append(uriToBeAppended);
                foreach (var parameter in queryString)
                {
                    if (parameter.Value == null) continue;

                    sb.Append(hasQuery ? '&' : '?');
                    sb.Append(UrlEncoder.Default.Encode(parameter.Key));
                    sb.Append('=');
                    sb.Append(UrlEncoder.Default.Encode(parameter.Value));
                    hasQuery = true;
                }

                sb.Append(anchorText);
                return sb.ToString();
            }
        }
    }
}
