using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignaturePadSample.Xaml
{
    public class CustomSignaturePadView : SignaturePadView
    {
        public static readonly BindableProperty StrokesJsonProperty;
        public static readonly BindableProperty SignatureCommandProperty;

        static CustomSignaturePadView()
        {
            StrokesJsonProperty = BindableProperty.Create(
                   nameof(StrokesJson),
                   typeof(string),
                   typeof(CustomSignaturePadView),
                   null,
                   propertyChanged: (bindable, oldValue, newValue) => ((CustomSignaturePadView)bindable).SignaturePadCanvas.Strokes = ParseStrokes((string)newValue));

            SignatureCommandProperty = BindableProperty.Create(
            nameof(StrokeCompletedCommand),
            typeof(ICommand),
            typeof(CustomSignaturePadView),
            default(ICommand));
        }

        public CustomSignaturePadView()
        {
            SignaturePadCanvas.StrokeCompleted += delegate
            {
                OnStrokeCompleted();
            };

            SignaturePadCanvas.Cleared += delegate
            {
                OnCleared();
            };
        }

        private async void OnStrokeCompleted()
        {
            if (SignatureCommand != null && SignatureCommand.CanExecute(null))
            {
                await Task.Delay(300);
                var strokes = JsonSerializer.Serialize(this.Strokes);
                var stream = await this.GetImageStreamAsync(SignatureImageFormat.Png);
                SignatureCommand.Execute(new Tuple<string, Stream>(strokes, stream));
            }
        }

        private void OnCleared()
        {
            if (SignatureCommand != null && SignatureCommand.CanExecute(null))
            {
                SignatureCommand.Execute(null);
            }
        }

        private static IEnumerable<IEnumerable<Point>> ParseStrokes(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Enumerable.Empty<IEnumerable<Point>>();
            }
            return JsonSerializer.Deserialize<IEnumerable<IEnumerable<Point>>>(value);
        }

        public string StrokesJson
        {
            get => (string)GetValue(StrokesJsonProperty);
            set => SetValue(StrokesJsonProperty, value);
        }

        public ICommand SignatureCommand
        {
            get => (ICommand)GetValue(SignatureCommandProperty);
            set => SetValue(SignatureCommandProperty, value);
        }
    }
}