using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;


[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(Xamarin_Samples.UWP.Extensions.RoundEffect), nameof(Xamarin_Samples.UWP.Extensions.RoundEffect))]
namespace Xamarin_Samples.UWP.Extensions
{
    public class RoundEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
           
        }

        protected override void OnDetached()
        {
        }
    }
}
