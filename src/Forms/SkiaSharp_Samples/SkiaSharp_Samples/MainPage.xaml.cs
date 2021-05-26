using SkiaSharp_Samples.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkiaSharp_Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnRotate_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BitmapRotateView());
        }

        private void btnResize_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BitmapResizeView());
        }

        private void btnAnnotate_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BitmapAnnotateView());
        }
    }
}
