using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin_Samples.Droid;
using Xamarin_Samples.Interfaces;


[assembly: Dependency(typeof(ApplicationContext))]
namespace Xamarin_Samples.Droid
{
    public class ApplicationContext : IApplicationContext
    {
        public void Exit()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}