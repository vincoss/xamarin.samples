using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Forms_WebView.Services;

namespace Xamarin_Forms_WebView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmbeddedResourceView : ContentPage
    {
        public EmbeddedResourceView()
        {
            InitializeComponent();
            HtmlSource();
        }

        private void HtmlSource()
        {
            var htmlSource = new HtmlWebViewSource();
            var path = "Xamarin_Forms_WebView.Resources.page001.html";
            htmlSource.Html = SampleExtensions.GetValueEmbeddedResource(typeof(EmbeddedResourceView).Assembly, path);
            webViewHtmlString.Source = htmlSource;
        }
    }
}