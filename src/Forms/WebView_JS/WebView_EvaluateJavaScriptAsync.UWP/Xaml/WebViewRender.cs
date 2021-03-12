//using WebView_EvaluateJavaScriptAsync.UWP.Xaml;
//using WebView_EvaluateJavaScriptAsync.Xaml;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.UWP;


////[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
////namespace WebView_EvaluateJavaScriptAsync.UWP.Xaml
////{
////    public class WebViewRender : WebViewRenderer
////    {
////        protected async override void OnElementChanged(ElementChangedEventArgs<WebView> e)
////        {
////            base.OnElementChanged(e);
////            var webView = e.NewElement as WebViewer;
////            if (webView != null)
////                webView.EvaluateJavascript = async (js) =>
////                {
////                    return await Control.InvokeScriptAsync("eval", new[] { js });
////                };
////        }
////    }
////}
