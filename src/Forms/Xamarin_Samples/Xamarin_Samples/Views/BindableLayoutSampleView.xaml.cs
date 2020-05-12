using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BindableLayoutSampleView : ContentPage
    {
        private readonly BindableLayoutSampleViewModel _model;

        public BindableLayoutSampleView()
        {
            InitializeComponent();

            _model = new BindableLayoutSampleViewModel();
            BindingContext = _model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _model.Items.Clear();

            for(int i = 0; i < 1000; i++)
            {
                _model.Items.Add(i);
            }
        }

        public class BindableLayoutSampleViewModel : BaseViewModel
        {
            public BindableLayoutSampleViewModel()
            {
                Items = new ObservableCollection<int>();
            }
            public ObservableCollection<int> Items { get; private set; }
        }
    }
}