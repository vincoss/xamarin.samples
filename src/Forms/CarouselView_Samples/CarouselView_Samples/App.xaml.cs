using CarouselView_Samples.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarouselView_Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CreateWorkflowView();
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
