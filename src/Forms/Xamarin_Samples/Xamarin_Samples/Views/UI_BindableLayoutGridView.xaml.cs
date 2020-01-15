using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_BindableLayoutGridView : ContentPage
    {
        public UI_BindableLayoutGridView()
        {
            InitializeComponent();

            BindingContext = new UI_BindableLayoutGridViewModel();
        }
    }

    public class UI_BindableLayoutGridViewModel
    { 
        public UI_BindableLayoutGridViewModel()
        {
            Items = "Populating a bindable layout with data".Split(new[] { ' ' }).ToArray();
        }

        public IEnumerable<string> Items { get; set; }
    }

}