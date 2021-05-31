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
        /// <summary>
        /// Points and Vectors
        /// </summary>
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

        /// <summary>
        /// Points and Vectors
        /// </summary>
        [Fact]
        public void SamplePointsAndVectors()
        {
            var point1 = new SKPoint(50, 50);
            var point2 = new SKPoint(110, 200);
            var point3 = new SKPoint(80, 80);
            var vector = point2 - point1;
            var point4 = SKPoint.Add(point3, vector);

            var p1 = new SKPath();
            p1.MoveTo(point1);
            p1.LineTo(point2);

            var p2 = new SKPath();
            p2.MoveTo(point3);
            p2.LineTo(point4);

            SkiaSharpHelper.DrawLines(new[] { p1, p2 });
        }

        // Vector Addition and Subtraction
        [Fact]
        public void TestAdditionSubtraction()
        {
            var point1 = new SKPoint(50, 0);
            var point2 = new SKPoint(40, 100);

            var point3 = new SKPoint(5, 135);
            var point4 = new SKPoint(75, 170);

            var vector1 = point2 - point1;
            var vector2 = point4 - point3;

            // Add
            var vectorA = vector1 + vector2;
            // Substract
            var vectorS = vector1 - vector2;

            var p1 = new SKPath();
            p1.MoveTo(point1);
            p1.LineTo(point2);

            var p2 = new SKPath();
            p2.MoveTo(point3);
            p2.LineTo(point4);

            SkiaSharpHelper.DrawLines(new[] { p1, p2 }, nameof(TestAdditionSubtraction));
        }

        //Vector Multiplication and Division
        [Fact]
        public void TestMultiplicationDivision()
        {
            var point1 = new SKPoint(50, 0);
            var point2 = new SKPoint(40, 100);
            var vector1 = point2 - point1;

            //// Multiple
            //var bigVector = vector1 * 3;

            //// Divide
            //var smallVector = bigVector / 3;
        }

            Random _rand = new Random();

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
                float lineWidth = _rand.Next(1, 10);
                var lineColor = new SKColor(
                        red: (byte)_rand.Next(255),
                        green: (byte)_rand.Next(255),
                        blue: (byte)_rand.Next(255),
                        alpha: (byte)_rand.Next(255));

                var linePaint = new SKPaint
                {
                    Color = lineColor,
                    StrokeWidth = lineWidth,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };

                int x1 = _rand.Next(imageInfo.Width);
                int y1 = _rand.Next(imageInfo.Height);
                int x2 = _rand.Next(imageInfo.Width);
                int y2 = _rand.Next(imageInfo.Height);
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
