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
       private readonly  Dictionary<long, DrawInfo> _inProgressPaths = new Dictionary<long, DrawInfo>();
       private readonly  List<DrawInfo> _completedPaths = new List<DrawInfo>();




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

        private DrawingType _drawingType = DrawingType.Line;

        private void SkCanvasView_Touch(object sender, SKTouchEventArgs args)
        {
            switch (args.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (!_inProgressPaths.ContainsKey(args.Id))
                    {
                        DrawInfo info = new DrawInfo();
                        SKPath path = new SKPath();
                        info.Path = path;
                        info.Type = _drawingType;
                        path.MoveTo(ConvertToPixel(args.Location));
                        _inProgressPaths.Add(args.Id, info);
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Moved:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        var info = _inProgressPaths[args.Id];
                        SKPath path = info.Path;
                        path.LineTo(ConvertToPixel(args.Location));
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Released:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        _completedPaths.Add(_inProgressPaths[args.Id]);
                        _inProgressPaths.Remove(args.Id);
                        UpdateBitmap();
                    }
                    break;

                case SKTouchAction.Cancelled:
                    if (_inProgressPaths.ContainsKey(args.Id))
                    {
                        _inProgressPaths.Remove(args.Id);
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

                foreach (DrawInfo info in _completedPaths)
                {
                    switch (info.Type)
                    {
                        case DrawingType.Line:
                            {
                                DrawLine(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Path:
                            {
                                DrawPath(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Polygon:
                            {
                                DrawPolygon(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Rectangle:
                            {
                                DrawRectangle(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Circle:
                            {
                                DrawCircle(info.Path, saveBitmapCanvas);
                                break;
                            }
                    }
                }

                foreach (DrawInfo info in _inProgressPaths.Values)
                {
                    switch (info.Type)
                    {
                        case DrawingType.Line:
                            {
                                DrawLine(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Path:
                            {
                                DrawPath(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Polygon:
                            {
                                DrawPolygon(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Rectangle:
                            {
                                DrawRectangle(info.Path, saveBitmapCanvas);
                                break;
                            }
                        case DrawingType.Circle:
                            {
                                DrawCircle(info.Path, saveBitmapCanvas);
                                break;
                            }
                    }
                }
            }

            canvasView.InvalidateSurface();
            lblType.Text = _drawingType.ToString();
        }

        private void DrawLine(SKPath path, SKCanvas canvas)
        {
            var start = path.Points.First();
            SKPoint end = path.LastPoint;

            if (start != end)
            {
                canvas.DrawLine(start, end, _paint);
            }
        }

        private void DrawPath(SKPath path, SKCanvas canvas)
        {
            canvas.DrawPath(path, _paint);
        }

        private void DrawPolygon(SKPath path, SKCanvas canvas)
        {
            path.Close();
            canvas.DrawPath(path, _paint);
        }

        private void DrawRectangle(SKPath path, SKCanvas canvas)
        {
            var start = path.Points.First();
            SKPoint end = path.LastPoint;

            var horizontalChange = start.X - end.X;
            var verticalChange = start.Y - end.Y;

            if(end.X < start.X)
            {
                start.X = end.X;
            }

            if(end.Y < start.Y)
            {
                start.Y = end.Y;
            }

            var w = Math.Abs(horizontalChange);
            var h = Math.Abs(verticalChange);

            SKRect skRectangle = new SKRect();
            skRectangle.Size = new SKSize(w, h);
            skRectangle.Location = start;

            canvas.DrawRect(skRectangle, _paint);
        }

        private void DrawCircle(SKPath path, SKCanvas canvas)
        {
            var start = path.Points.First();
            SKPoint end = path.LastPoint;

            var d = SKPoint.Distance(start, end);



            if (d == 0)
            {
                return;
            }

            var r =  Math.Abs(d / 2);

            if (end.X < start.X)
            {
                start.X = end.X;
            }

            if (end.Y < start.Y)
            {
                start.Y = end.Y;
            }

            start.X = start.X + r;
            start.Y = start.Y + r;

            canvas.DrawCircle(start, r, _paint);
        }

        #region Buttons

        private void btnPath_Clicked(object sender, EventArgs e)
        {
            _drawingType = DrawingType.Path;
        }

        private void btnLine_Clicked(object sender, EventArgs e)
        {
            _drawingType = DrawingType.Line;
        }

        private void btnPologon_Clicked(object sender, EventArgs e)
        {
            _drawingType = DrawingType.Polygon;
        }

        private void btnRectangle_Clicked(object sender, EventArgs e)
        {
            _drawingType = DrawingType.Rectangle;
        }

        private void btnCircle_Clicked(object sender, EventArgs e)
        {
            _drawingType = DrawingType.Circle;
        }

        private void btnArrow_Clicked(object sender, EventArgs e)
        {

        }

        private void btnPicker_Clicked(object sender, EventArgs e)
        {

        }

        #endregion

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

        public class DrawInfo
        {
            public DrawingType Type { get; set; }
            public SKPath Path { get; set; }
        }

        public enum DrawingType : byte
        {
            Line,
            Path,
            Polygon,
            Rectangle,
            Circle,
            Triangle,
            Arrow
        }
    }
}