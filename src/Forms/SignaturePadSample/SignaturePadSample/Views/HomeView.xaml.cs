using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignaturePadSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private IEnumerable<IEnumerable<Point>> _signatureStrokes;

        public HomeView()
        {
            InitializeComponent();
        }

        private void Init()
        {
            entryAppPath.Text = GetAppRootPath();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var bitmap = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
        }

        private void btnGetPoints_Clicked(object sender, EventArgs e)
        {
            _signatureStrokes = signatureView.Strokes;
        }

        private void btnRestore_Clicked(object sender, EventArgs e)
        {
           // signatureView.Strokes = newStrokes;
        }

        public static string GetAppRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

    }
}