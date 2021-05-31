using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaSharp_Samples
{
   public  class SkiaSharpHelper
    {
        public static void DrawLines(IEnumerable<SKPath> paths, string fileName = nameof(DrawLines))
        {
            const int width = 1000;
            const int height = 1000;

            var imageInfo = new SKImageInfo(
            width: width,
            height: height,
            colorType: SKColorType.Rgba8888,
            alphaType: SKAlphaType.Premul);
            var surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            canvas.Clear(SKColor.Parse("#003366"));

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                //  IsAntialias = true,
                Color = SKColors.Blue,
                StrokeWidth = 10,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round
            };

            foreach(var path in paths)
            {
                var start = path.Points.First();
                var end = path.LastPoint;

                canvas.DrawLine(start, end, paint);
            }

            var directory = AppContext.BaseDirectory;
            var fullPath = Path.Combine(directory, $"{fileName}.jpg");

            using (var image = surface.Snapshot())
            using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
            using (var stream = File.OpenWrite(fullPath))
            {
                // save the data to a stream
                data.SaveTo(stream);
            }

        }
    }
}
