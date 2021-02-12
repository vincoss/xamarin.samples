using Default_FlyoutTemplate.Services;
using Default_FlyoutTemplate.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Default_FlyoutTemplate
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
