using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Forms.OAuthSamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, EventArgs args)
        {
            SampleOne();
        }

        /// <summary>
        /// OAuth2 Implicit Grant flow
        /// </summary>
        private void SampleOne()
        {
            var auth = new OAuth2Authenticator(
               clientId: "lofbktt2ah7hu89",
               scope: "account_info.read",
               authorizeUrl: new Uri("https://www.dropbox.com/oauth2/authorize"),
               redirectUrl: new Uri(App.AppUrl),
               isUsingNativeUI: false);

            auth.Completed += Auth_Completed;
            auth.Error += Auth_Error;

            var presenter = new OAuthLoginPresenter();
            presenter.Login(auth);
        }

        private void Auth_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                // Use eventArgs.Account to do wonderful things
            }
            else
            {
                // The user cancelled
            }
        }

        private void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if(e.IsAuthenticated)
            {
                var token = e.Account.Properties["access_token"];

                // Use eventArgs.Account to do wonderful things
            }
            else
            {
                // The user cancelled
            }
        }
    }
}