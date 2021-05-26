using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkiaSharp_Samples.ViewModels
{
    public class BitmapRotateViewModel : BaseViewModel
    {
        public BitmapRotateViewModel() 
        {
            AppRootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            CurrentPhotoPath = Path.Combine(AppRootPath, "monkey.jpg");
        }

        private string _appRootPath;

        public string AppRootPath
        {
            get { return _appRootPath; }
            set { SetProperty(ref _appRootPath, value); }
        }

        private string _currentPhotoPath;

        public string CurrentPhotoPath
        {
            get { return _currentPhotoPath; }
            set { SetProperty(ref _currentPhotoPath, value); }
        }
    }
}
