using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Forms_MultipleDeviceResolutions.Views;

namespace Xamarin_Forms_MultipleDeviceResolutions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TermscView();
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
