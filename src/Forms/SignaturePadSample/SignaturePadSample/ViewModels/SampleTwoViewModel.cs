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
        public SampleTwoViewModel()
        {
            SignatureCommand = new Command(OnSignatureCommand);
        }


        private async void OnSignatureCommand()
        {
            // Sample only; Use IOC to resolve view and models

            var view = new SignatureView();
            var model = new SignatureViewModel();
            view.BindingContext = model;
            model.SetSelection = SignatureSet;

            await App.Current.MainPage.Navigation.PushModalAsync(view);
        }

        public void SignatureSet(Tuple<string, Stream> signature)
        {
            // Just save for the sample
            var signatureFilePath = GetSignatureImgPath();
            var signatureStrokesFilePath = GetSignatureStrokesPath();

            if (File.Exists(signatureFilePath))
            {
                File.Delete(signatureFilePath);
            }

            if (signature != null)
            {
                using (var fw = new FileStream(signatureFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    signature.Item2.CopyTo(fw);
                }

                File.WriteAllText(signatureStrokesFilePath, signature.Item1);
            }

            ShowSignature = File.Exists(signatureFilePath);
            SignatureUrl = signatureFilePath;
        }

        private static string GetSignatureImgPath()
        {
            return Path.Combine(GetAppRootPath(), $"Signature.png");
        }

        private static string GetSignatureStrokesPath()
        {
            return Path.Combine(GetAppRootPath(), $"Signature.json");
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
