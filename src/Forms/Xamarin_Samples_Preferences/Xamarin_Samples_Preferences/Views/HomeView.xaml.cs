using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples_Preferences.Services;

namespace Xamarin_Samples_Preferences.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public const string DefaultPage = "OneView";
        private readonly ISettingsService _settingsService = new SettingService();

        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Preferences.Set(DefaultPage, nameof(OneView));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Preferences.Remove(DefaultPage);
        }

        private void settingSet_Clicked(object sender, EventArgs e)
        {
            _settingsService.StartupPage = nameof(OneView);
        }

        private void settingReset_Clicked(object sender, EventArgs e)
        {
            _settingsService.StartupPage = null;
        }
    }
}
