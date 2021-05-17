using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeolocationSample
{
    public partial class App : Application
    {
        public static readonly GeolocationService GeoService = new GeolocationService();

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            GeoService.Run();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
