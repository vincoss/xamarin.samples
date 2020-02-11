using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples_Lifecycle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ApplicationContext.Add($"{nameof(MainPage)}.OnAppearing");

            editorTxt.Text = ApplicationContext.GetMessageString();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ApplicationContext.Add($"{nameof(MainPage)}.OnDisappearing");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OneView());
        }
    }
}
