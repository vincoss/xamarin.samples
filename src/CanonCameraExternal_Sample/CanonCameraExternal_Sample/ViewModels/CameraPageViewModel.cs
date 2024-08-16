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
    public interface ICloseable
    {
        void Close();
    }

    public class CameraPageViewModel : BaseViewModel
    {
        private ICanonEosCameraService _cameraService;
        private Stream _stream;
        private bool _isPhoto = false;
        private bool _isDownloaded = false;
        private bool _disposed = false;
        private readonly CancellationTokenSource _cts;
        private SKBitmap _liveViewCanvasBackground = new SKBitmap();

        public CameraPageViewModel(ICanonEosCameraService cameraService, CancellationTokenSource cst)
        {
            _cameraService = cameraService;
            _cts = cst;

            TakePhotoCommand = new Command(OnTakePhotoCommand, OnCanTakePhotoCommand);
            UsePhotoCommand = new Command<ICloseable>(OnUsePhotoCommand, OnCanUsePhotoCommand);
            RetakePhotoCommand = new Command(OnRetakePhotoCommand, OnCanRetakePhotoCommand);
            CancelCommand = new Command<ICloseable>(OnCancelCommand, OnCanCancelCommand);
        }

        public override void Initialize()
        {
            _cameraService.Run(ActivityAction);
        }

        private void ActivityAction(CameraArg info)
        {
            //UpdateLiveView(info);
            //UpdateHeading(info);
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
            return IsBusy == false && PhotoState == State.Live && _isPhoto == false;
        }

        private void OnUsePhotoCommand(ICloseable window)
        {
            try
            {
                if (OnCanUsePhotoCommand(window) == false)
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
                window.Close();
                IsBusy = false;
            }
        }

        private bool OnCanUsePhotoCommand(ICloseable window)
        {
            return IsBusy == false && window != null && _stream != null && _stream.Length > 0;
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
                PhotoState = State.Live;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool OnCanRetakePhotoCommand()
        {
            return IsBusy == false && PhotoState != State.Live && _isPhoto && _isDownloaded;
        }

        private void OnCancelCommand(ICloseable window)
        {
            if (OnCanCancelCommand(window) == false)
            {
                throw new InvalidOperationException(nameof(OnCanCancelCommand));
            }

            window.Close();
        }

        private bool OnCanCancelCommand(ICloseable window)
        {
            return window != null;
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

        private State _state = State.Loading;
        public State PhotoState
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        public SKBitmap LiveViewCanvasBackground
        {
            get { return _liveViewCanvasBackground; }
            set { SetProperty(ref _liveViewCanvasBackground, value); }
        }

    }
}
