using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignaturePadSample.Xaml
{
    public class CustomSignaturePadView : SignaturePadView
    {
        public static readonly BindableProperty SignatureCommandProperty;

        static CustomSignaturePadView()
        {
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
                string strokes = null;
                if (Strokes != null && Strokes.Any())
                {
                    strokes = JsonSerializer.Serialize(this.Strokes);
                }
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

        public ICommand SignatureCommand
        {
            get => (ICommand)GetValue(SignatureCommandProperty);
            set => SetValue(SignatureCommandProperty, value);
        }
    }
}
