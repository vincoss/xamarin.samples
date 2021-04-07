using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SignaturePadSample.Xaml
{
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

            control.TextChanged += (sender, e) =>
            {
                var s = sender as SearchBar;
                var command = GetTextChangedCommand(s);

                command.Execute(e.NewTextValue);
            };
        }
    }
}
