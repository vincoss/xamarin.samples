using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SignaturePadSample.Xaml
{
    /*
        push strokes
        get strokes
        get stream
    */

    public class SignaturePadViewAttached
    {
        public static BindableProperty StrokesProperty =
             BindableProperty.CreateAttached("Strokes", typeof(string), typeof(Nullable), null, propertyChanged: HandleChanged);

        public static string GetStrokes(BindableObject view)
        {
            return (string)view.GetValue(StrokesProperty);
        }

        public static void SetStrokes(BindableObject view, string value)
        {
            view.SetValue(StrokesProperty, value);
        }

        static void HandleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SignaturePadView;

            if (control == null)
            {
                return;
            }
            var points = control.Strokes;
            //control.TextChanged += (sender, e) =>
            //{
            //    var s = sender as SearchBar;
            //    var command = GetTextChangedCommand(s);

            //    command.Execute(e.NewTextValue);
            //};
        }
    }

    public interface ISignaturePadView
    {
       // string StrokesJson { get; set; }
        Task<Stream> GetSignature();
    }

    public class CustomSignaturePadView : SignaturePadView, ISignaturePadView
    {
        public static BindableProperty StrokesJsonProperty = BindableProperty.Create(
                nameof(StrokesJson),
                typeof(string),
                typeof(CustomSignaturePadView),
                null,
                propertyChanged: (bindable, oldValue, newValue) => ((CustomSignaturePadView)bindable).SignaturePadCanvas.Strokes = ParseStrokes((string)newValue));

        private static IEnumerable<IEnumerable<Point>> ParseStrokes(string strokesJson)
        {
            if(string.IsNullOrWhiteSpace(strokesJson))
            {
                return null;
            }
            return JsonSerializer.Deserialize<IEnumerable<IEnumerable<Point>>>(strokesJson);
        }



        /// <summary>
        /// Gets or sets the strokes.
        /// </summary>
        public string StrokesJson
        {
            get => (string)GetValue(StrokesJsonProperty);
            set => SetValue(StrokesJsonProperty, value);
        }

        static void HandleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SignaturePadView;

            if (control == null)
            {
                return;
            }
            var points = control.Strokes;
            //control.TextChanged += (sender, e) =>
            //{
            //    var s = sender as SearchBar;
            //    var command = GetTextChangedCommand(s);

            //    command.Execute(e.NewTextValue);
            //};
        }

        public async Task<Stream> GetSignature()
        {
            return await GetImageStreamAsync(SignatureImageFormat.Png);
        }
    }

}
