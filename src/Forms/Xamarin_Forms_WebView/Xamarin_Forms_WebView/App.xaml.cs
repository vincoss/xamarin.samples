using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Forms_WebView.Views;

namespace Xamarin_Forms_WebView
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomeView());
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
    }
}
