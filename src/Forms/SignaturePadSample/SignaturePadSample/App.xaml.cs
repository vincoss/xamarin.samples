using SignaturePadSample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignaturePadSample
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
