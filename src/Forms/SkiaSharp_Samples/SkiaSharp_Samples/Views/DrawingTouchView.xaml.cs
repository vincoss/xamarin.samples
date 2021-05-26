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
    public partial class DrawingTouchView : ContentPage
    {
       private readonly  Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
       private readonly  List<SKPath> completedPaths = new List<SKPath>();

      private readonly SKPaint _paint = new SKPaint
      {
          Style = SKPaintStyle.Stroke,
          IsAntialias = true,
          Color = SKColors.Blue,
          StrokeWidth = 10,
          StrokeCap = SKStrokeCap.Round,
          StrokeJoin = SKStrokeJoin.Round
      };

        private SKBitmap saveBitmap;

        public DrawingTouchView()
        {
            InitializeComponent();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            // Create bitmap the size of the display surface
            if (saveBitmap == null)
            {
                saveBitmap = new SKBitmap(info.Width, info.Height);
            }

            // Or create new bitmap for a new size of display surface
            else if (saveBitmap.Width < info.Width || saveBitmap.Height < info.Height)
            {
                var newBitmap = new SKBitmap(Math.Max(saveBitmap.Width, info.Width), Math.Max(saveBitmap.Height, info.Height));

                using (var newCanvas = new SKCanvas(newBitmap))
                {
                    newCanvas.Clear();
                    newCanvas.DrawBitmap(saveBitmap, 0, 0);
                }

                saveBitmap = newBitmap;
            }

            // Render the bitmap
            canvas.Clear();
            canvas.DrawBitmap(saveBitmap, 0, 0);
        }

        private void SkCanvasView_Touch(object sender, SKTouchEventArgs args)
        {
            switch (args.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (!inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        inProgressPaths.Add(args.Id, path);
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id];
                        path.LineTo(ConvertToPixel(args.Location));
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Released:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        completedPaths.Add(inProgressPaths[args.Id]);
                        inProgressPaths.Remove(args.Id);
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Cancelled:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        UpdateBitmap();
                    }
                    break;
            }
        }

        private SKPoint ConvertToPixel(SKPoint pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        private void UpdateBitmap()
        {
            using (SKCanvas saveBitmapCanvas = new SKCanvas(saveBitmap))
            {
                saveBitmapCanvas.Clear();

                foreach (SKPath path in completedPaths)
                {
                    saveBitmapCanvas.DrawPath(path, _paint);
                }

                foreach (SKPath path in inProgressPaths.Values)
                {
                    saveBitmapCanvas.DrawPath(path, _paint);
                }
            }

            canvasView.InvalidateSurface();
        }

        public static void DrawLine(SKCanvas canvas, SKPoint p0, SKPoint p1)
        {
            using (var skPaint = new SKPaint())
            {
                skPaint.Style = SKPaintStyle.Stroke;
                skPaint.IsAntialias = true;
                skPaint.Color = SKColors.Red;
                skPaint.StrokeWidth = 10;
                skPaint.StrokeCap = SKStrokeCap.Round;

                canvas.DrawLine(p0, p1, skPaint);
            }
        }

        //private SKPoint? _startTouchPoint = new SKPoint();
        //private SKPoint? _lastTouchPoint = new SKPoint();

        //public DrawingTouchView()
        //{
        //    InitializeComponent();
        //}

        //private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        //{
        //    if(_startTouchPoint == null)
        //    {
        //        return;
        //    }

        //    SKImageInfo info = args.Info;
        //    SKSurface surface = args.Surface;
        //    SKCanvas canvas = surface.Canvas;
        //    canvas.Clear();

        //    // Drawing Stroke
        //    using (SKPaint skPaint = new SKPaint())
        //    {
        //        skPaint.Style = SKPaintStyle.Stroke;
        //        skPaint.IsAntialias = true;
        //        skPaint.Color = SKColors.Red;
        //        skPaint.StrokeWidth = 10;
        //        skPaint.StrokeCap = SKStrokeCap.Round;

        //        canvas.DrawLine(_startTouchPoint.Value, _lastTouchPoint.Value, skPaint);
        //    }
        //}

        //Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        //List<SKPath> completedPaths = new List<SKPath>();
        //long touchId = -1;

        //private void SkCanvasView_Touch(object sender, SKTouchEventArgs args)
        //{
        //    lblTouchId.Text = args.Id.ToString();

        //    switch (args.ActionType)
        //    {
        //        case SKTouchAction.Pressed:
        //            {
        //                touchId = args.Id;

        //                _startTouchPoint = args.Location;
        //                break;
        //            }
        //        case SKTouchAction.Moved:
        //            {
        //                if (touchId == args.Id && args.InContact)
        //                {
        //                    _lastTouchPoint = args.Location;
        //                }
        //                break;
        //            }

        //        case SKTouchAction.Released:
        //        case SKTouchAction.Cancelled:
        //            {
        //                touchId = -1;
        //            }
        //            break;
        //    }

        //    canvasView.InvalidateSurface();
        //}

        //public static void DrawPath(SKCanvas canvas, SKPath path)
        //{
        //    using (var skPaint = new SKPaint())
        //    {
        //        skPaint.Style = SKPaintStyle.Stroke;
        //        skPaint.IsAntialias = true;
        //        skPaint.Color = SKColors.Blue;
        //        skPaint.StrokeWidth = 10;
        //        skPaint.StrokeCap = SKStrokeCap.Round;
        //        skPaint.StrokeJoin = SKStrokeJoin.Round;

        //        canvas.DrawPath(path, skPaint);

        //    }
        //}

        //public static void DrawLine(SKCanvas canvas, SKPoint p0, SKPoint p1)
        //{
        //    using (var skPaint = new SKPaint())
        //    {
        //        skPaint.Style = SKPaintStyle.Stroke;
        //        skPaint.IsAntialias = true;
        //        skPaint.Color = SKColors.Red;
        //        skPaint.StrokeWidth = 10;
        //        skPaint.StrokeCap = SKStrokeCap.Round;

        //        canvas.DrawLine(p0, p1, skPaint);
        //    }
        //}

        // DrawPoints (SKPointMode mode, points, paint)

        public enum DrawingType : byte
        {
            Path,
            Line,
            Lines,
            Square,
            Circle,
            Arrow,
            Triangle
        }

    }
}