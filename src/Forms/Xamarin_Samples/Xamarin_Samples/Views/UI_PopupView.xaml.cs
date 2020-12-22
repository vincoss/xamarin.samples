using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_PopupView : ContentPage
    {
        public UI_PopupView()
        {
            InitializeComponent();
        }

        private async void DisplayAlert_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "He there!!!", "CANCEL");
        }

        private async void DisplayPromptAsync_Clicked(object sender, EventArgs e)
        {
            await DisplayPromptAsync("Create Tag Group", null, placeholder: "Tag group name");
        }

        private async void DisplayActionSheet_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("Actions", "Cancel", "Destruction", "Edit", "Delete", "Report", "Copy", "Share");
            lblAction.Text = result;
        }
    }
}