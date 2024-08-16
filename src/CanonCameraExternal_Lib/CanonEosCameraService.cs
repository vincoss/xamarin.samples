using EOSDigital.API;
using EOSDigital.SDK;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CanonCameraExternal_Lib
{
    public enum State
    {
        Loading,
        Live,
        Stop,
        Shutdown,
        Photo,
        Error
    }

    public class CameraArg
    {
        public CameraArg()
        {
            Name = "No Camera";
        }

        public string Name { get; set; }

        public bool IsLiveViewOn
        {
            get { return CameraState == State.Live; }
        }
        public State CameraState { get; set; }
        public Stream Photo { get; set; }
        public SKBitmap Live { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface ICanonEosCameraService : IDisposable
    {
        void Run(Action<CameraArg> activity);
        void TakePhoto();
        void StartLiveView();
        void StopLiveView();
    }

    public class CanonEosCameraService : ICanonEosCameraService
    {
        private CanonAPI _CanonAPI;
        private EOSDigital.API.Camera _ActiveCamera;
        private Action<CameraArg> _activity;
        private bool _disposed = false;

        public void Run(Action<CameraArg> activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));
            _activity = activity;

            _CanonAPI = new CanonAPI();
            _CanonAPI.CameraAdded += _CanonAPI_CameraAdded;

            StartPreview();
        }

        public void TakePhoto()
        {
            if (IsCameraStillAvailable(_ActiveCamera) == false)
            {
                return;
            }

            _ActiveCamera.TakePhotoAsync();
        }

        public void StartLiveView()
        {
            if (IsCameraStillAvailable(_ActiveCamera) == false)
            {
                return;
            }

            _ActiveCamera.StartLiveView();
        }

        public void StopLiveView()
        {
            if (IsCameraStillAvailable(_ActiveCamera) == false)
            {
                return;
            }

            _ActiveCamera.StopLiveView();
        }

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
                    DisposeCameraPreview();
                    DisposeCanonApi();
                }

                _disposed = true;
            }
        }

        #endregion

        #region Private methods


        private void StartPreview()
        {
            if (_CanonAPI == null)
            {
                throw new InvalidOperationException("CannotAPI not initialized. Call Initialize method first.");
            }

            // Stop existing preview, will switch camera.
            DisposeCameraPreview();

            var devices = _CanonAPI.GetCameraList();
            _ActiveCamera = devices.FirstOrDefault();

            if (_ActiveCamera == null)
            {
                NotifyErrorActivity("No Camera Available");
                return;
            }

            NotifyLoadingActivity();
            _ActiveCamera.LiveViewUpdated += OnActiveCamera_LiveViewUpdated;
            _ActiveCamera.LiveViewStopped += OnActiveCamera_LiveViewStopped;
            _ActiveCamera.CameraHasShutdown += OnActiveCamera_CameraHasShutdown;
            _ActiveCamera.DownloadReady += OnActiveCamera_DownloadReady;
            _ActiveCamera.OpenSession();
            _ActiveCamera.StartLiveView();
            _ActiveCamera.SetSetting(PropertyID.SaveTo, (int)SaveTo.Host);
            _ActiveCamera.SetCapacity(4096, int.MaxValue);
        }

        private void _CanonAPI_CameraAdded(CanonAPI sender)
        {
            StartPreview();
        }

        private void DisposeCanonApi()
        {
            if (_CanonAPI != null)
            {
                _CanonAPI.Dispose();
                _CanonAPI = null;
            }
        }

        private void DisposeCameraPreview()
        {
            if (_ActiveCamera == null || _ActiveCamera.IsDisposed)
            {
                return;
            }

            _ActiveCamera.LiveViewUpdated -= OnActiveCamera_LiveViewUpdated;
            _ActiveCamera.LiveViewStopped -= OnActiveCamera_LiveViewStopped;
            _ActiveCamera.CameraHasShutdown -= OnActiveCamera_CameraHasShutdown;
            _ActiveCamera.DownloadReady -= OnActiveCamera_DownloadReady;
            _ActiveCamera.CloseSession();
            _ActiveCamera.Dispose();
            _ActiveCamera = null;
        }

        private CameraArg GetActivity()
        {
            var args = new CameraArg();

            if (IsCameraStillAvailable(_ActiveCamera))
            {
                args.Name = _ActiveCamera.DeviceName;
            }

            return args;
        }

        private void NotifyErrorActivity(string errorMessage)
        {
            var arg = GetActivity();
            arg.CameraState = State.Error;
            arg.ErrorMessage = errorMessage;
            _activity(arg);
        }

        private void NotifyLoadingActivity()
        {
            var arg = GetActivity();
            arg.CameraState = State.Loading;
            _activity(arg);
        }

        private void NotifyStopedActivity()
        {
            var arg = GetActivity();
            arg.CameraState = State.Stop;
            _activity(arg);
        }

        private void NotifyShutdownActivity()
        {
            var arg = GetActivity();
            arg.CameraState = State.Shutdown;
            _activity(arg);
        }

        private void NotifyPhotoActivity(Stream stream)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Position = 0;
            var arg = GetActivity();
            arg.CameraState = State.Photo;
            arg.Photo = ms;
            _activity(arg);
        }

        private void NotifyLiveActivity(SKBitmap image)
        {
            var arg = GetActivity();
            arg.CameraState = State.Live;
            arg.Live = image;
            _activity(arg);
        }

        #endregion

        #region Canon Sdk event handlers

        private void OnActiveCamera_LiveViewUpdated(EOSDigital.API.Camera sender, Stream img)
        {
            if (IsCameraStillAvailable(sender) == false)
            {
                return;
            }

            using (var s = new WrapStream(img))
            {
                img.Position = 0;

                var skData = SKData.Create(s);
                var codec = SKCodec.Create(skData);
                var original = SKBitmap.Decode(codec);

                NotifyLiveActivity(original);
            }
        }

        private void OnActiveCamera_LiveViewStopped(EOSDigital.API.Camera sender)
        {
            NotifyStopedActivity();
        }

        private void OnActiveCamera_CameraHasShutdown(EOSDigital.API.Camera sender)
        {
            NotifyShutdownActivity();
        }

        private void OnActiveCamera_DownloadReady(EOSDigital.API.Camera sender, DownloadInfo Info)
        {
            if (IsCameraStillAvailable(sender) == false)
            {
                return;
            }

            sender.StopLiveView(); // NOTE: stop after photo taken

            using (var cameraStream = sender.DownloadFile(Info))
            {
                NotifyPhotoActivity(cameraStream);
            }
        }

        private bool IsCameraStillAvailable(EOSDigital.API.Camera camera)
        {
            if (camera == null || camera.IsDisposed || camera.SessionOpen == false)
            {
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// A stream that does nothing more but wrap another stream (supposeadly needed for a WPF memory leak according to EOS sample)
        /// </summary>
        public sealed class WrapStream : Stream
        {
            /// <summary>
            /// Gets a value indicating whether the current stream supports reading.
            /// </summary>
            public override bool CanRead
            {
                get { return Base.CanRead; }
            }
            /// <summary>
            /// Gets a value indicating whether the current stream supports seeking.
            /// </summary>
            public override bool CanSeek
            {
                get { return Base.CanSeek; }
            }
            /// <summary>
            /// Gets a value indicating whether the current stream supports writing.
            /// </summary>
            public override bool CanWrite
            {
                get { return Base.CanWrite; }
            }
            /// <summary>
            /// Gets the length in bytes of the stream.
            /// </summary>
            public override long Length
            {
                get { return Base.Length; }
            }
            /// <summary>
            /// Gets or sets the position within the current stream.
            /// </summary>
            public override long Position
            {
                get { return Base.Position; }
                set { Base.Position = value; }
            }

            private Stream Base;

            /// <summary>
            /// Creates a new instance of the <see cref="WrapStream"/> class.
            /// </summary>
            /// <param name="inStream">The stream that gets wrapped</param>
            public WrapStream(Stream inStream)
            {
                Base = inStream;
            }

            /// <summary>
            /// reads a sequence of bytes from the current stream and advances
            /// the position within the stream by the number of bytes read.
            /// </summary>
            /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified
            /// byte array with the values between offset and (offset + count - 1) replaced
            /// by the bytes read from the current source.</param>
            /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read
            /// from the current stream.</param>
            /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
            /// <returns>The total number of bytes read into the buffer. This can be less than the
            /// number of bytes requested if that many bytes are not currently available,
            /// or zero (0) if the end of the stream has been reached.</returns>
            public override int Read(byte[] buffer, int offset, int count)
            {
                return Base.Read(buffer, offset, count);
            }

            /// <summary>
            /// When overridden in a derived class, writes a sequence of bytes to the current
            /// stream and advances the current position within this stream by the number
            /// of bytes written.
            /// </summary>
            /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
            /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the
            /// current stream.</param>
            /// <param name="count">The number of bytes to be written to the current stream.</param>
            public override void Write(byte[] buffer, int offset, int count)
            {
                Base.Write(buffer, offset, count);
            }

            /// <summary>
            /// sets the position within the current stream.
            /// </summary>
            /// <param name="offset">A byte offset relative to the origin parameter.</param>
            /// <param name="origin">A value of type System.IO.SeekOrigin indicating the reference point used
            /// to obtain the new position.</param>
            /// <returns>The new position within the current stream.</returns>
            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                return Base.Seek(offset, origin);
            }

            /// <summary>
            /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
            /// </summary>
            public override void Flush()
            {
                Base.Flush();
            }

            /// <summary>
            /// Sets the length of the current stream.
            /// </summary>
            /// <param name="value">The desired length of the current stream in bytes.</param>
            public override void SetLength(long value)
            {
                Base.SetLength(value);
            }
        }
    }
}
