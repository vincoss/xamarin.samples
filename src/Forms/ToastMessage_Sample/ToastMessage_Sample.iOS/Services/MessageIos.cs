using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToastMessage_Sample.Interface;
using ToastMessage_Sample.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIos))]
namespace ToastMessage_Sample.iOS.Services
{
    public class MessageIos : IMessage
    {
        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        public MessageIos()
        {
        }

        public void LongAlert(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            ShowAlert(message, LONG_DELAY);
        }

        public void ShortAlert(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            ShowAlert(message, SHORT_DELAY);
        }

        private void ShowAlert(string message, double seconds)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissMessage(alert, obj);
            });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage(UIAlertController alert, NSTimer alertDelay)
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }

            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}