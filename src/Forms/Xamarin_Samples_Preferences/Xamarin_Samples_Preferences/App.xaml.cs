using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples_Preferences.Services;
using Xamarin_Samples_Preferences.Views;

namespace Xamarin_Samples_Preferences
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Setting_Preferences();
        }

        public void Sample_Preferences()
        {
            var key = HomeView.DefaultPage;

            var value = Preferences.Get(key, null);

            Page page = null;
            if (value == nameof(OneView))
            {
                page = new OneView();
            }
            else
            {
                page = new HomeView();
            }

            MainPage = new NavigationPage(page);
        }

        public void Setting_Preferences()
        {
            var settings = new SettingService();
            var value = settings.StartupPage;

            Page page = null;
            if (value == nameof(OneView))
            {
                page = new OneView();
            }
            else
            {
                page = new HomeView();
            }

            MainPage = new NavigationPage(page);
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
