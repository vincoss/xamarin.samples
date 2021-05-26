using SkiaSharp;
using SkiaSharp.Views.Forms;
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
    public partial class DrawingCircleView : ContentPage
    {
        public DrawingCircleView()
        {
            InitializeComponent();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            // Drawing Stroke
            using (SKPaint skPaint = new SKPaint())
            {
                skPaint.Style = SKPaintStyle.Fill;
                skPaint.IsAntialias = true;
                skPaint.Color = SKColors.Blue;
                skPaint.StrokeWidth = 10;

                canvas.DrawCircle(0, 0, 70, skPaint);
            }
        }
    }
}