using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Localization.Resources;

namespace Xamarin_Localization
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Sample();
        }

        public void Sample()
        {
            AppResources.Culture = new CultureInfo("sk-SK");

            // Using Resources in Code
            var myLabel = new Label();
            var myEntry = new Entry();
            // populate UI with translated text values from resources
            myLabel.Text = AppResources.WeekDayKey;
            myEntry.Placeholder = AppResources.WeekDayPlaceholderKey;

            localizationSample.Children.Add(myLabel);
            localizationSample.Children.Add(myEntry);
        }
    }
}
