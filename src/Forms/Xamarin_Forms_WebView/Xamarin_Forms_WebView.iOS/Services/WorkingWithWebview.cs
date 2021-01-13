using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UIKit;
using Xamarin_Forms_WebView.iOS.Services;
using Xamarin_Forms_WebView.Services;


[assembly: Xamarin.Forms.Dependency(typeof(BaseUrl_iOS))]
namespace Xamarin_Forms_WebView.iOS.Services
{
	public class BaseUrl_iOS : IBaseUrl
	{
		public string Get()
		{
			return $"{NSBundle.MainBundle.BundlePath}/page001/";
		}
	}
}