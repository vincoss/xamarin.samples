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
    public partial class SourceHtmlStringView : ContentPage
    {
        public SourceHtmlStringView()
        {
            InitializeComponent();
            HtmlSource();
        }

        private void HtmlSource()
        {
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html>
                                    <body>
                                        <h1>Xamarin.Forms</h1>
                                        <p>Welcome to WebView.</p>
                                    </body>
                                </html>";
            webViewHtmlString.Source = htmlSource;
        }
    }
}