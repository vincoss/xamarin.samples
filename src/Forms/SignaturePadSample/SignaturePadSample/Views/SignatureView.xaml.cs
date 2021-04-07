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
            signaturePadView.Clear();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var strokes = signaturePadView.Strokes;
            var stream = await signaturePadView.GetImageStreamAsync(SignatureImageFormat.Png);

            var model = BindingContext as SignatureViewModel;
            if (model == null)
            {
                return;
            }

            var arg = new SignatureArg
            {
                Strokes = JsonSerializer.Serialize(strokes),
                Image = stream
            };

            model.SaveCommand.Execute(arg);
            await Navigation.PopModalAsync();
        }

    }
}