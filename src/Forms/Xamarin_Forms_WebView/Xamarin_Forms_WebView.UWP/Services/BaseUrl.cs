using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Forms_WebView.Services;
using Xamarin_Forms_WebView.UWP.Services;


[assembly: Xamarin.Forms.Dependency(typeof(BaseUrl))]
namespace Xamarin_Forms_WebView.UWP.Services
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///assets/page001/";
        }
    }
}
