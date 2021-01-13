using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Forms_WebView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SourceUrlVIew : ContentPage
    {
        public SourceUrlVIew()
        {
            InitializeComponent();
        }

        private void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            webView.Reload();
        }
    }
}