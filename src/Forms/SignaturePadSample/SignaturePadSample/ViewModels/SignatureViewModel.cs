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
        public SignatureViewModel()
        {
            SaveCommand = new Command<SignatureArg>(OnSaveCommand);
        }

        private void OnSaveCommand(SignatureArg args)
        {
            //if(args == null)
            //{
            //    throw new ArgumentNullException(nameof(args));
            //}



            //var filePath = Path.Combine(GetAppRootPath(), $"Signature.png");

            //if(File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}

            //if (args.Image != null)
            //{
            //    using (var fw = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            //    {
            //        args.Image.CopyTo(fw);
            //    }
            //}

            //ShowSignature = File.Exists(filePath);
            //SignatureUrl = filePath;
        }

        public static string GetAppRootPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public ICommand SaveCommand { get; private set; }

        private string _storkes;

        public string Strokes
        {
            get { return _storkes; }
            set { SetProperty(ref _storkes, value); }
        }

        public Func<string> GetSelection { get; set; }
        public Action<Tuple<Stream, string>> SetSelection { get; set; }
    }

    public class SignatureArg
    {
        public string Strokes { get; set; }
        public Stream Image { get; set; }
    }
}
