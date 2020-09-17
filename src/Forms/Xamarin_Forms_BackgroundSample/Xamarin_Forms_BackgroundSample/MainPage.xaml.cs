using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Forms_BackgroundSample.Services;


namespace Xamarin_Forms_BackgroundSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnGetInfo_Clicked(object sender, EventArgs e)
        {
            var last = PeriodicService.Runs.LastOrDefault();
            if(last == DateTime.MinValue)
            {
                lblInfo.Text = "Never";
            }
            else
            {
                lblInfo.Text = last.ToString();
            }
        }
    }
}
