using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignaturePadSample.ViewModels
{
    public class SignatureViewModel : BaseViewModel
    {
        private Tuple<string, Stream> _actualSignature;

        public SignatureViewModel()
        {
            SaveCommand = new Command(OnSaveCommand);
            SignatureCommand = new Command<Tuple<string, Stream>>(OnSignatureCommand);
        }

        public override void Initialize()
        {
            StrokesJson = GetSelection();
        }

        private void OnSaveCommand()
        {
            SetSelection(_actualSignature);
        }

        private void OnSignatureCommand(Tuple<string, Stream> arg)
        {
            _actualSignature = arg;
        }

        public static string GetAppRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand SignatureCommand { get; private set; }

        private string _strokeJson;

        public string StrokesJson
        {
            get { return _strokeJson; }
            set { SetProperty(ref _strokeJson, value); }
        }

        public Func<string> GetSelection { get; set; }
        public Action<Tuple<string, Stream>> SetSelection { get; set; }
    }
}
