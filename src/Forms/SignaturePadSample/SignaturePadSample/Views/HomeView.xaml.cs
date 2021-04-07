using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SignaturePadSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            if(btn.Text == "Sample One")
            {
                Navigation.PushAsync(new SampleOne());
            }

            if (btn.Text == "Sample Two")
            {
                Navigation.PushAsync(new SampleTwo());
            }
        }
    }
}