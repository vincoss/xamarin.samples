using AsyncAwaitBestPractices;
using AsyncCommandMvvm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsyncCommandMvvm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var model = new SampleViewModel();
            BindingContext = model;

            model.Initialize().SafeFireAndForget();
        }
    }
}
