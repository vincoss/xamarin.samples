using SkiaSharp_Samples.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharp_Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new DrawingTouchView());
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
