using Microsoft.Identity.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms_EnterpriseApp_Authentication.Views;

namespace XamarinForms_EnterpriseApp_Authentication
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
