using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebView_EvaluateJavaScriptAsync
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnRun_Clicked(object sender, EventArgs e)
        {
            var result = await hybridWebView.EvaluateJavaScriptAsync("var runMe = function(){ return 'minjin';}; runMe();");
            lblError.Text = string.IsNullOrWhiteSpace(result) ? "Empty" : result;
        }
    }
}