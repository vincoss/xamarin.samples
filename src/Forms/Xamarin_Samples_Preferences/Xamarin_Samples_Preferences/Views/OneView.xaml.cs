using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples_Preferences.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneView : ContentPage
    {
        public OneView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeView());
        }
    }
}