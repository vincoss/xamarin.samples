using MultipleDeviceResolutions.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultipleDeviceResolutions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomeView();
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
