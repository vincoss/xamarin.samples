using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Windows.UI.Xaml;
using Xamarin_Samples.Interfaces;
using Xamarin_Samples.UWP;

[assembly: Dependency(typeof(ApplicationContext))]
namespace Xamarin_Samples.UWP
{
    public class ApplicationContext : IApplicationContext
    {
        public void Exit()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
