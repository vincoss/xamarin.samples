using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_UX.Views;

namespace Xamarin_UX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ValidationView());
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
