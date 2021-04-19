using NetGenericHost.Interfaces;
using NetGenericHost.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetGenericHost
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            await BootstrapExtensions.Run(new string[0]);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
