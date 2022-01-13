using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples_Lifecycle.Views
{
    public partial class OneView : ContentPage
    {
        public OneView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ApplicationContext.Add($"{nameof(OneView)}.OnAppearing");

            editorTxt.Text = ApplicationContext.GetMessageString();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ApplicationContext.Add($"{nameof(OneView)}.OnDisappearing");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}