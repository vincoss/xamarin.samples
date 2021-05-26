using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharp_Samples.SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharp_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BitmapResizeView : ContentPage
    {
        PhotoCropperCanvasView photoCropper;
        SKBitmap croppedBitmap;

        public BitmapResizeView()
        {
            InitializeComponent();

            var bitmap = BitmapExtensions.LoadBitmapResource(typeof(BitmapRotateView), "SkiaSharp_Samples.Resources.Banana.jpg");

            photoCropper = new PhotoCropperCanvasView(bitmap);
            canvasViewHost.Children.Add(photoCropper);
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(croppedBitmap, info.Rect, BitmapStretch.Uniform);
        }

        private void btnDone_Clicked(object sender, EventArgs e)
        {
            croppedBitmap = photoCropper.CroppedBitmap;

            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }
    }
}