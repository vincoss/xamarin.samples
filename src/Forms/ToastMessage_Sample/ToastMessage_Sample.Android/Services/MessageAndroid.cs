using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToastMessage_Sample.Droid.Services;
using ToastMessage_Sample.Interface;


[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace ToastMessage_Sample.Droid.Services
{
    public class MessageAndroid : IMessage
    {
        public MessageAndroid()
        {
        }
        public void ShortAlert(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void LongAlert(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}