using MaterialVisual_Samples.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialVisual_Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new FloatingLabelView());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new FloatingLabelSampleView());
        }
    }
}
