using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Default_Empty_AppCenter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenterRegister();

            MainPage = new MainPage();

        }

        private void AppCenterRegister()
        {
            AppCenter.Start("8fcd6028-c612-4822-93f5-65a278dfda5e", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnStart()
        {
            Analytics.TrackEvent(nameof(OnStart));
        }

        protected override void OnSleep()
        {
            Analytics.TrackEvent(nameof(OnSleep));
        }

        protected override void OnResume()
        {
            Analytics.TrackEvent(nameof(OnResume));
        }
    }
}
