using SignaturePadSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignaturePadSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleTwo : ContentPage
    {
        private SampleTwoViewModel _model = new SampleTwoViewModel();

        public SampleTwo()
        {
            InitializeComponent();

            BindingContext = _model;

            this.Appearing += SampleTwo_Appearing;
        }

        private void SampleTwo_Appearing(object sender, EventArgs e)
        {
            _model.Initialize();
        }
    }
}