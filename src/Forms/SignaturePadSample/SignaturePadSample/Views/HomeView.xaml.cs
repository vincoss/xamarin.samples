using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            Init();
        }

        private void Init()
        {
            entryAppPath.Text = GetAppRootPath();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
            
            if (stream == null) // Might be null if not signed
            {
                return;
            }

            var filePath = Path.Combine(GetAppRootPath(), $"Signature.png");
            using (var fw = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.CopyTo(fw);
            }
        }

        private void btnGetPoints_Clicked(object sender, EventArgs e)
        {
            _signatureStrokes = signatureView.Strokes;
            entryStrokes.Text = JsonSerializer.Serialize(_signatureStrokes);
        }

        private void btnRestore_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(entryStrokes.Text))
            {
                return;
            }
            signatureView.Strokes = JsonSerializer.Deserialize<IEnumerable<IEnumerable<Point>>>(entryStrokes.Text);
        }

        public static string GetAppRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

    }
}