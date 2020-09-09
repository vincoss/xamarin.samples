using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;

namespace Xamarin_Auth_Samples
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

        private void btnMicrosoft_Clicked(object sender, EventArgs e)
        {
            var clientId = "TODO - client ID";
            var scope = "User.Read";
            var tenant = "TODO - tenant";
            var redirectUrl = "com.companyname.webauthenticatorsamples://oauth2redirect";

            var auth = new OAuth2Authenticator(
            clientId: clientId,
            scope: scope,
            authorizeUrl: new Uri($"https://login.microsoftonline.com/{tenant}/oauth2/authorize"),
            redirectUrl: new Uri(redirectUrl),
            isUsingNativeUI: false);

            auth.Completed += Authenticator_Completed;
            auth.Error += Authenticator_Error;

            // NOTE: Null reference thrown if missing
            // Add this into App.cs  global::Xamarin.Auth.Presenters.UWP.AuthenticationConfiguration.Init();
            var presenter = new OAuthLoginPresenter();
            presenter.Login(auth);
        }

        private void Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            lblInfo.Text = e.Message;
        }

        private void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
           
        }
    }
}
