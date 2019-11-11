using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin_Samples.Interfaces;
using Xamarin_Samples.iOS;

[assembly: Dependency(typeof(ApplicationContext))]
namespace Xamarin_Samples.iOS
{
    public class ApplicationContext : IApplicationContext
    {
        public void Exit()
        {
            Thread.CurrentThread.Abort();
        }
    }
}