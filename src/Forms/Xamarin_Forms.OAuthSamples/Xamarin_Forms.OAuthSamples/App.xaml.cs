using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Forms.OAuthSamples.Views;

namespace Xamarin_Forms.OAuthSamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (!uri.ToString().ToLowerInvariant().StartsWith(AppUrl, StringComparison.Ordinal))
                return;

            base.OnAppLinkRequestReceived(uri);
        }

        //io.identitymodel.native:/oauth2redirect
        public const string AppUrl = "http://localhost:/oauth2redirect";
    }
}
