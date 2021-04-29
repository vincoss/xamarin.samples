using NetGenericHost.Interfaces;
using NetGenericHost.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetGenericHost
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
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
