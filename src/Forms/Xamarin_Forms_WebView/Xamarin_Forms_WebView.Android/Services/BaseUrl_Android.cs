using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin_Forms_WebView.Droid.Services;
using Xamarin_Forms_WebView.Services;


[assembly: Xamarin.Forms.Dependency(typeof(BaseUrl_Android))]
namespace Xamarin_Forms_WebView.Droid.Services
{
	public class BaseUrl_Android : IBaseUrl
	{
		public string Get()
		{
			return "file:///android_asset/page001/";
		}
	}
}