using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WebAuthenticatorSamples
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonLoginApple_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Version.Major >= 13)
                {
                    // Use Native Apple Sign In API's
                    var authResult = await AppleSignInAuthenticator.AuthenticateAsync();

                    var accessToken = authResult?.AccessToken;
                    lblInfo.Text = accessToken;
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            var type = ((Button)sender).Text;

            switch (type)
            {
                case "Dropbox":
                    {
                        Dropbox();
                        break;
                    }
                case "Google":
                    {
                        Google();
                        break;
                    }
                case "Microsoft":
                    {
                        Microsoft();
                        break;
                    }
                default:
                    throw new InvalidOperationException($"Invalid provider: {type}");
            }
        }

        private void Dropbox()
        {
            var u = DropboxConfiguration.AuthorityUrl;
            var c = DropboxConfiguration.ClientId;
            var r = DropboxConfiguration.ResponseType;
            var b = DropboxConfiguration.RedirectUri;
            var s = DropboxConfiguration.Scope;

            Login(u, c, r, b, s);
        }

        private void Google()
        {
            var u = GoogleConfiguration.AuthorityUrl;
            var c = GoogleConfiguration.ClientId;
            var r = GoogleConfiguration.ResponseType;
            var b = GoogleConfiguration.RedirectUri;
            var s = GoogleConfiguration.Scope;

            Login(u, c, r, b, s);
        }

        private void Microsoft()
        {
            var u = MicrosoftConfiguration.AuthorityUrl;
            var c = MicrosoftConfiguration.ClientId;
            var r = MicrosoftConfiguration.ResponseType;
            var b = MicrosoftConfiguration.RedirectUri;
            var s = MicrosoftConfiguration.Scope;

            Login(u, c, r, b, s);
        }

        private async void Login(string url, string clientId, string responseType, string callback, string scope)
        {
            try
            {
                var loginService = new LoginService();
                var fullUrl = loginService.BuildAuthenticationUrl(url, clientId, responseType, callback, scope);

                var authenticationResult = await WebAuthenticator.AuthenticateAsync(
                         new Uri(fullUrl),
                         new Uri(callback));

                var accessToken = authenticationResult?.AccessToken;

                if(accessToken == null && authenticationResult.Properties != null && authenticationResult.Properties.ContainsKey("code"))
                {
                    accessToken = authenticationResult.Properties["code"];
                }

                lblInfo.Text = accessToken;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }
    }

    /// <summary>
    /// https://www.dropbox.com/developers/apps
    /// https://www.dropbox.com/developers/documentation/http/documentation
    /// </summary>
    public class DropboxConfiguration
    {
        public const string ClientId = ""; //App key
        public const string AuthorityUrl = "https://www.dropbox.com/oauth2/authorize";
        public const string RedirectUri = "com.companyname.webauthenticatorsamples:/oauth2redirect";
        public const string ResponseType = "token";
        public const string Scope = "account_info.read";
    }

    /// <summary>
    /// https://console.developers.google.com/
    /// https://support.google.com/cloud/answer/6158849?hl=en
    /// https://github.com/googlesamples/oauth-apps-for-windows
    /// https://timothelariviere.com/2017/09/01/authenticate-users-through-google-with-xamarin-auth/
    /// https://developers.google.com/identity/protocols/oauth2/native-app#uwp
    /// https://www.syncfusion.com/blogs/post/google-login-integration-in-xamarin-forms-a-complete-guide.aspx
    /// https://timothelariviere.com/2017/09/01/authenticate-users-through-google-with-xamarin-auth/
    /// https://github.com/xamarin/Xamarin.Auth/blob/master/docs/readme.md
    /// https://developers.google.com/identity/protocols/oauth2/native-app
    /// https://stackoverflow.com/questions/tagged/google-oauth
    /// https://github.com/xamarin/Xamarin.Auth/blob/master/docs/readme.md
    /// https://dev.to/theonlybeardedbeast/using-google-drive-in-a-c-application-38ag
    /// https://afterlogic.com/mailbee-net/docs/OAuth2UWP.html
    /// </summary>
    public class GoogleConfiguration
    {
        public const string ClientId = ""; //App key
        public const string AuthorityUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        public const string RedirectUri = "com.companyname.webauthenticatorsamples:/oauth2redirect";
        public const string ResponseType = "code";
        public const string Scope = "openid";
    }

    /// <summary>
    /// https://devblogs.microsoft.com/xamarin/enterprise-apps-made-easy-updated-libraries-apis/
    /// https://login.microsoftonline.com/{tenantID}/.well-known/openid-configuration
    /// https://forums.xamarin.com/discussion/176473/login-with-xamarin-auth-using-microsoft-account-does-not-redirect
    /// </summary>
    public class MicrosoftConfiguration
    {
        public const string ClientId = "APPID"; //App key
        public const string AuthorityUrl = "https://login.microsoftonline.com/{tennant}/oauth2/authorize";
        public const string RedirectUri = "com.companyname.webauthenticatorsamples://oauth2redirect";
        public const string ResponseType = "code";
        public const string Scope = "openid";
    }

    public class LoginService
    {
        private string codeVerifier;
        private const string CodeChallengeMethod = "S256";

        public string BuildAuthenticationUrl(string authorizeUrl, string clientId, string responseType, string callback, string scope)
        {
            var state = CreateCryptoGuid();
            var codeChallenge = CreateCodeChallenge();
            var sb = new StringBuilder();

            // Build URL
            sb.Append(authorizeUrl);
            sb.Append($"?client_id={clientId}");
            sb.Append($"&response_type={responseType}");
            sb.Append($"&redirect_uri={callback}");
            sb.Append($"&scope={scope}");        
            // TODO:
            //sb.Append($"&state={state}");
            //sb.Append($"&code_challenge={codeChallenge}");
            // sb.Append($"&code_challenge_method={CodeChallengeMethod}");

            var url = sb.ToString();
            return url;
        }

        private string CreateCryptoGuid()
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                generator.GetBytes(bytes);
                return new Guid(bytes).ToString("N");
            }
        }

        private string CreateCodeChallenge()
        {
            codeVerifier = CreateCryptoGuid();
            using (var sha256 = SHA256.Create())
            {
                var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                return Convert.ToBase64String(codeChallengeBytes);
            }
        }

        public JwtSecurityToken ParseAuthenticationResult(WebAuthenticatorResult authenticationResult)
        {
            if (authenticationResult == null) throw new ArgumentNullException(nameof(authenticationResult));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authenticationResult.IdToken);
            return token;
        }
    }

}
