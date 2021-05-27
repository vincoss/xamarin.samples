using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace SkiaSharp_Samples
{
    public class VectorGeometryTest
    {
        [Fact]
        public void PointsAndVectors()
        {
            var point1 = new SKPoint(50, 50);
            var point2 = new SKPoint(110, 200);

            var vector = point2 - point1;
            var distance = SKPoint.Distance(point1, point2);

            Assert.Equal("{X=60, Y=150}", vector.ToString());
            Assert.Equal(161.55495, Math.Round(distance, 5));
        }

        Random rand = new Random();

        [Fact]
        public void SampleDraw()
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

            int lineCount = 1000;
            for (int i = 0; i < lineCount; i++)
            {
                float lineWidth = rand.Next(1, 10);
                var lineColor = new SKColor(
                        red: (byte)rand.Next(255),
                        green: (byte)rand.Next(255),
                        blue: (byte)rand.Next(255),
                        alpha: (byte)rand.Next(255));

                var linePaint = new SKPaint
                {
                    Color = lineColor,
                    StrokeWidth = lineWidth,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };

                int x1 = rand.Next(imageInfo.Width);
                int y1 = rand.Next(imageInfo.Height);
                int x2 = rand.Next(imageInfo.Width);
                int y2 = rand.Next(imageInfo.Height);
                canvas.DrawLine(x1, y1, x2, y2, linePaint);
            }

            var path = AppContext.BaseDirectory;
            var fullPath = Path.Combine(path, $"{nameof(SampleDraw)}.jpg");

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
