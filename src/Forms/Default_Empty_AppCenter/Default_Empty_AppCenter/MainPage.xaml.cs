using Default_Empty_AppCenter.Views;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Default_Empty_AppCenter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnTestCrash_Clicked(object sender, EventArgs e)
        {
            Crashes.GenerateTestCrash();
        }

        private void btnExceptionBasic_Clicked(object sender, EventArgs e)
        {
            try
            {
                var x = 0;
                var a = 2 / x;
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        private void btnExceptionWithData_Clicked(object sender, EventArgs e)
        {
            try
            {

                var x = 0;
                var a = 2 / x;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string> {
    { "Category", "Music" },
    { "Wifi", "On" }
  };
                Crashes.TrackError(exception, properties);
            }
        }

        private void btnNavigateViewOne_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OneView());
        }
    }
}
