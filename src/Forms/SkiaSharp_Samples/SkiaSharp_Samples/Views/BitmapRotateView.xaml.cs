using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharp_Samples.SkiaSharp;
using SkiaSharp_Samples.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SkiaSharp_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BitmapRotateView : ContentPage
    {
        private const double _defaultRotation = 90.0;
        private double _currentRotation = 0;

        static readonly SKBitmap originalBitmap = BitmapExtensions.LoadBitmapResource(typeof(BitmapRotateView), "SkiaSharp_Samples.Resources.Banana.jpg");
        SKBitmap rotatedBitmap = originalBitmap;

        private readonly BitmapRotateViewModel _model = new BitmapRotateViewModel();

        public BitmapRotateView()
        {
            InitializeComponent();

            BindingContext = _model;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(rotatedBitmap, info.Rect, BitmapStretch.Uniform);
        }

        private void btnRotate_Clicked(object sender, EventArgs e)
        {
            if (_currentRotation >= 360)
            {
                _currentRotation = 0;
            }
            _currentRotation += _defaultRotation;

            double radians = Math.PI * _currentRotation / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = originalBitmap.Width;
            int originalHeight = originalBitmap.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (SKCanvas canvas = new SKCanvas(rotatedBitmap))
            {
                canvas.Clear(SKColors.LightPink);
                canvas.Translate(rotatedWidth / 2, rotatedHeight / 2);
                canvas.RotateDegrees((float)_currentRotation);
                canvas.Translate(-originalWidth / 2, -originalHeight / 2);
                canvas.DrawBitmap(originalBitmap, new SKPoint());
            }

            canvasView.InvalidateSurface();
        }

        private void btnDone_Clicked(object sender, EventArgs e)
        {
            using (var image = SKImage.FromBitmap(rotatedBitmap))
            {
                using (var output = File.OpenWrite(_model.CurrentPhotoPath))
                {
                    image.Encode(SKEncodedImageFormat.Jpeg, 100).SaveTo(output);
                }
            }
        }
    }
}