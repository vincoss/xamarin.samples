using SignaturePadSample.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignaturePadSample.ViewModels
{
    public class SampleTwoViewModel : BaseViewModel
    {
        private static string _tempStrokes;

        public SampleTwoViewModel()
        {
            SignatureCommand = new Command(OnSignatureCommand);
        }


        private async void OnSignatureCommand()
        {
            var view = new SignatureView();
            var model = new SignatureViewModel();
            view.BindingContext = model;
            model.GetSelection = SignatureGet;
            model.SetSelection = SignatureSet;

            await App.Current.MainPage.Navigation.PushModalAsync(view);

        }

        public string SignatureGet()
        {
            return _tempStrokes;
        }

        public void SignatureSet(Tuple<Stream, string> signature)
        {
            //_tempStrokes = strokes;

            //// Just save for the sample
            //var filePath = Path.Combine(GetAppRootPath(), $"Signature.png");

            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}

            //if (stream != null)
            //{
            //    using (var fw = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            //    {
            //        stream.CopyTo(fw);
            //    }
            //}

            //ShowSignature = File.Exists(filePath);
            //SignatureUrl = filePath;
        }

        public static string GetAppRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }



        public ICommand SignatureCommand { get; private set; }


        private string _signatureUrl;

        public string SignatureUrl
        {
            get { return _signatureUrl; }
            set { SetProperty(ref _signatureUrl, value); }
        }

        private bool _showSignature;

        public bool ShowSignature
        {
            get { return _showSignature; }
            set { SetProperty(ref _showSignature, value); }
        }
    }
}
