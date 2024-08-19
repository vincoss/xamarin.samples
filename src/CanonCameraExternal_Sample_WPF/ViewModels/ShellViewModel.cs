using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using CanonCameraExternal_Sample_WPF.Xaml;
using CanonCameraExternal_Sample_WPF.Services;

namespace CanonCameraExternal_Sample_WPF.ViewModels
{
    public interface ICloseable
    {
        void Close();
    }

    public class ShellViewModel : BaseViewModel, IDisposable
    {
        private ICanonEosCameraService _cameraService;
        private Stream _stream;
        private bool _isPhoto = false;
        private bool _isDownloaded = false;
        private bool _disposed = false;
        private readonly CancellationTokenSource _cts;
        private ImageBrush _liveViewCanvasBackground = new ImageBrush();

        public ShellViewModel(ICanonEosCameraService cameraService, CancellationTokenSource cst)
        {
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
            _cts = cst ?? throw new ArgumentNullException(nameof(cst));

            TakePhotoCommand = new DelegateCommand(OnTakePhotoCommand, OnCanTakePhotoCommand);
            UsePhotoCommand = new DelegateCommand<ICloseable>(OnUsePhotoCommand, OnCanUsePhotoCommand);
            RetakePhotoCommand = new DelegateCommand(OnRetakePhotoCommand, OnCanRetakePhotoCommand);
            CancelCommand = new DelegateCommand<ICloseable>(OnCancelCommand, OnCanCancelCommand);

            _liveViewCanvasBackground.Stretch = Stretch.Uniform;

            PropertyChanged += ShellViewModel_PropertyChanged;
        }

        public override void Initialize()
        {
            _cameraService.Run(ActivityAction);
        }

        #region Events

        private void ShellViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ((Command)this.TakePhotoCommand).RaiseCanExecuteChanged();
            ((Command)this.UsePhotoCommand).RaiseCanExecuteChanged();
            ((Command)this.RetakePhotoCommand).RaiseCanExecuteChanged();
            ((Command)this.CancelCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Private methods

        private void ActivityAction(CameraArg info)
        {
            UpdateLiveView(info);
            UpdateHeading(info);
        }

        private void UpdateLiveView(CameraArg info)
        {
            if (info.IsLiveViewOn)
            {
                var delegateMethod = new Action<BitmapImage>((a) =>
                {
                    _liveViewCanvasBackground.ImageSource = a;
                });
                App.Current.Dispatcher.BeginInvoke(delegateMethod, info.Live);
            }
            else // Take photo completed, (download)
            {
                if (_isPhoto && info.Photo != null)
                {
                    _isDownloaded = true;
                    _stream = info.Photo;
                    SetCanvasImageToStream(info.Photo);
                }
            }
        }

        private void UpdateHeading(CameraArg info)
        {
            var state = info.CameraState;

            // NOTE: Photo capture turn off the live preview and that triggers new nottification, if photo keep photo state.
            if (_isPhoto)
            {
                state = State.Photo;
            }

            var sb = new StringBuilder();
            sb.Append($"{info.Name} - {state}");

            if (_isPhoto && _isDownloaded == false)
            {
                sb.Append(" - (Downloading)");
            }

            if (string.IsNullOrEmpty(info.ErrorMessage) == false)
            {
                sb.Append($"- {info.ErrorMessage}");
            }

            var delegateMethod = new Action(() =>
            {
                PhotoState = state;
                Heading = sb.ToString();
            });

            App.Current.Dispatcher.BeginInvoke(delegateMethod);
        }

        private void SetCanvasImageToStream(Stream img)
        {
            var evfImage = new BitmapImage();
            using (var s = new WrapStream(img))
            {
                img.Position = 0;
                evfImage.BeginInit();
                evfImage.StreamSource = s;
                evfImage.CacheOption = BitmapCacheOption.OnLoad;
                evfImage.EndInit();
                evfImage.Freeze();
            }

            var delegateMethod = new Action<BitmapImage>((a) =>
            {
                _liveViewCanvasBackground.ImageSource = a;
                OnPropertyChanged(nameof(Heading));
            });
            App.Current.Dispatcher.BeginInvoke(delegateMethod, evfImage);
        }

        #endregion

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

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_stream != null)
                    {
                        _stream.Dispose();
                        _stream = null;
                    }

                    _cts.Cancel();
                    _cts.Dispose();
                    _cameraService.Dispose();

                    _disposed = true;
                }
            }
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

        public ImageBrush LiveViewCanvasBackground
        {
            get { return _liveViewCanvasBackground; }
            set { SetProperty(ref _liveViewCanvasBackground, value); }
        }
    }
}
