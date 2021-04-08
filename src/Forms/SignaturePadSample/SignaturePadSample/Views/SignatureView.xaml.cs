using SignaturePad.Forms;
using SignaturePadSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignaturePadSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignatureView : ContentPage
    {
        public SignatureView()
        {
            InitializeComponent();

            BindingContext = new SignatureViewModel();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            signatureViews.Clear();
        }
    }
}