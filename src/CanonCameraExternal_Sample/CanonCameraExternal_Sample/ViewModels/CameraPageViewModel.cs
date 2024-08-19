using CanonCameraExternal_Lib;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace CanonCameraExternal_Sample.ViewModels
{
    public class CameraPageViewModel : BaseViewModel
    {
        private ICanonEosCameraService _cameraService;
        private Stream _stream;
        private bool _isPhoto = false;
        private bool _isDownloaded = false;
        private bool _disposed = false;
        private readonly CancellationTokenSource _cts;
        private SKBitmap _liveViewCanvasBackground = new SKBitmap();
        private State _photoState = State.Loading;

        public CameraPageViewModel(ICanonEosCameraService cameraService, CancellationTokenSource cst)
        {
            _cameraService = cameraService;
            _cts = cst;

            TakePhotoCommand = new Command(OnTakePhotoCommand, OnCanTakePhotoCommand);
            UsePhotoCommand = new Command(OnUsePhotoCommand, OnCanUsePhotoCommand);
            RetakePhotoCommand = new Command(OnRetakePhotoCommand, OnCanRetakePhotoCommand);
            CancelCommand = new Command(OnCancelCommand, OnCanCancelCommand);

            this.PropertyChanged += CameraPageViewModel_PropertyChanged;
        }

        private void CameraPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PhotoStateLive = true;//_photoState == State.Live || _photoState == State.Stop;
           // PhotoStatePreview = false;//_photoState == State.Photo;

            ((Command)this.TakePhotoCommand).ChangeCanExecute();
            ((Command)this.UsePhotoCommand).ChangeCanExecute();
            ((Command)this.RetakePhotoCommand).ChangeCanExecute();
            ((Command)this.CancelCommand).ChangeCanExecute();
        }

        public override void Initialize()
        {
            _cameraService.Run(ActivityAction);
        }

        private void ActivityAction(CameraArg info)
        {
            UpdateLiveView(info);
            //UpdateHeading(info);
        }

        private void UpdateLiveView(CameraArg info)
        {
            if (info.IsLiveViewOn)
            {

            }
            else // Take photo completed, (download)
            {
                if (_isPhoto && info.Photo != null)
                {
                    _isDownloaded = true;
                    _stream = info.Photo;
                }
            }
        }

        private void UpdateHeading(CameraArg info)
        {
        }

        #region Command methods

        private void OnTakePhotoCommand()
        {
            if (OnCanTakePhotoCommand() == false)
            {
                throw new InvalidOperationException(nameof(OnCanTakePhotoCommand));
            }
            try
            {
                IsBusy = true;
                _stream = null; // Reset
                _isDownloaded = false;
                _cameraService.TakePhoto();
            }
            finally
            {
                _isPhoto = true;
                IsBusy = false;
            }
        }

        private bool OnCanTakePhotoCommand()
        {
            return IsBusy == false && _photoState == State.Live && _isPhoto == false;
        }

        private async void OnUsePhotoCommand()
        {
            try
            {
                if (OnCanUsePhotoCommand() == false)
                {
                    throw new InvalidOperationException(nameof(OnCanUsePhotoCommand));
                }

                // NOTE: user might take lots of fotos but use only one

                IsBusy = true;
                using (var fileStream = File.Create(PhotoPath))
                {
                    _stream.Seek(0, SeekOrigin.Begin);
                    _stream.CopyTo(fileStream);
                }
            }
            finally
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
                IsBusy = false;
            }
        }

        private bool OnCanUsePhotoCommand()
        {
            return IsBusy == false && _stream != null && _stream.Length > 0;
        }

        private void OnRetakePhotoCommand()
        {
            if (OnCanRetakePhotoCommand() == false)
            {
                throw new InvalidOperationException(nameof(OnCanRetakePhotoCommand));
            }
            try
            {
                IsBusy = true;
                _isPhoto = false;
                _cameraService.StartLiveView();
                _photoState = State.Live;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool OnCanRetakePhotoCommand()
        {
            return IsBusy == false && _photoState != State.Live && _isPhoto && _isDownloaded;
        }

        private async void OnCancelCommand()
        {
            if (OnCanCancelCommand() == false)
            {
                throw new InvalidOperationException(nameof(OnCanCancelCommand));
            }

           await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private bool OnCanCancelCommand()
        {
            return true;
        }

        #endregion

        #region Commands

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand UsePhotoCommand { get; private set; }
        public ICommand RetakePhotoCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        private string _photoPath;
        public string PhotoPath
        {
            get { return _photoPath; }
            set { SetProperty(ref _photoPath, value); }
        }

        private string _heading;
        public string Heading
        {
            get { return _heading; }
            set { SetProperty(ref _heading, value); }
        }

        private bool _photoStateLive = false;
        public bool PhotoStateLive
        {
            get { return _photoStateLive; }
            set { SetProperty(ref _photoStateLive, value); }
        }

        private bool _photoStatePreview = false;
        public bool PhotoStatePreview
        {
            get { return _photoStateLive; }
            set { SetProperty(ref _photoStatePreview, value); }
        }

        public SKBitmap LiveViewCanvasBackground
        {
            get { return _liveViewCanvasBackground; }
            set { SetProperty(ref _liveViewCanvasBackground, value); }
        }

    }
}