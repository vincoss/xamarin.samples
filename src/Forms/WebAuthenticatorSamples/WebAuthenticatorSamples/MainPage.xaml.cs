using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
          await  OnAuthenticate("");
        }

        const string authenticationUrl = "https://www.dropbox.com/oauth2/authorize?client_id=lofbktt2ah7hu89&response_type=token&redirect_uri=WebAuthenticatorSamples";

        async Task OnAuthenticate(string scheme)
        {
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    r = await AppleSignInAuthenticator.AuthenticateAsync();
                }
                else
                {
                    var authUrl = new Uri(authenticationUrl);
                    var callbackUrl = new Uri("WebAuthenticatorSamples://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                lblInfo.Text = r?.AccessToken ?? r?.IdToken;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }
    }

    public class LoginService
    {
        private string codeVerifier;
        private const string IDToken = "id_token";
        private const string CodeChallengeMethod = "S256";

        public string BuildAuthenticationUrl(string authorizeUrl, string clientId)
        {
            //var state = CreateCryptoGuid();
            //var nonce = CreateCryptoGuid();
            //var codeChallenge = CreateCodeChallenge();

            return $"{authorizeUrl}?client_id&{clientId}7response_type=token&redirect_uri=WebAuthenticatorSamples:/";

            //return $"{Configuration.OrganizationUrl}/oauth2/default/v1/authorizeresponse_type={IDToken}ope=openid%20profile" +
            //    $"&redirect_uri={ConfigurationCallback}" +
            //    $"&client_id={Configuration.ClientId}state=tate}" +
            //    $"&code_challenge{codeChallenge}" +
            //    $"&code_challenge_method={CodeChallengeMethod}" +
            //    $"&nonce={nonce}";
        }

        //private string CreateCryptoGuid()
        //{
        //    using (var generator = System.Security.Cryptography.RandomNumberGenerator.Create())
        //    {
        //        var bytes = new byte[16];
        //        generator.GetBytes(bytes);
        //        return new Guid(bytes).ToString("N");
        //    }
        //}

        //private string CreateCodeChallenge()
        //{
        //    codeVerifier = CreateCryptoGuid();
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetByte(codeVerifier));
        //        return Convert.ToBase64String(codeChallengeBytes);
        //    }
        //}

        //public JwtSecurityToken ParseAuthenticationResult(WebAuthenticatorResultauthenticationResult)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(authenticationResult.IdToken);
        //    return token;
        //}
    }

    
}
