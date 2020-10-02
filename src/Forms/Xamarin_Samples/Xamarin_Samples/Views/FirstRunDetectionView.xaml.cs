using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/xamarin/essentials/version-tracking?WT.mc_id=firstrun-blog-jamont
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstRunDetectionView : ContentPage
    {
        public FirstRunDetectionView()
        {
            InitializeComponent();

            VersionTracking.Track();
        }

        public void Detect()
        {
            if (VersionTracking.IsFirstLaunchEver)
            {
                // Display pop-up alert for first launch
            }
            else if (VersionTracking.IsFirstLaunchForCurrentVersion)
            {
                // Display update notification for current version (1.0.0)
            }
            else if (VersionTracking.IsFirstLaunchForCurrentBuild)
            {
                // Display update notification for current build number (2)
            }
        }
    }
}