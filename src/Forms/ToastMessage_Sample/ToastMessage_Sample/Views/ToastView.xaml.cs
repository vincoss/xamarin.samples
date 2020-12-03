using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToastMessage_Sample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastView : ContentView
    {
        public ToastView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ToastMessageProperty = BindableProperty.Create("ToastMessage", typeof(string), typeof(ToastView), null, propertyChanged: OnToastMessageChanged);

        public string ToastMessage
        {
            get { return (string)GetValue(ToastMessageProperty); }
            set { SetValue(ToastMessageProperty, value); }
        }

        private async static void OnToastMessageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
            {
                return;
            }

            var value = (string)newValue;
            var view = (ToastView)bindable;

            view.toastMessage.Text = value;

            // Fade in
            await view.FadeTo(1, 0);
            view.IsVisible = true;

            // Fede out
            await view.FadeTo(0, 1500);
            view.IsVisible = false;

            view.ToastMessage = null;
        }
    }

    public class ToastViewModel
    {
        public bool IsRunning { get; set; }
        public string Text { get; set; }
    }
}