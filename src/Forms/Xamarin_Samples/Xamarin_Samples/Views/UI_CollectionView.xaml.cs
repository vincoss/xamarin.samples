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
    public partial class UI_CollectionView : ContentPage
    {
        private UI_CollectionViewModel _viewModel = new UI_CollectionViewModel();

        public UI_CollectionView()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        private void addItem_Clicked(object sender, EventArgs e)
        {
            _viewModel.Add();
        }

        public class UI_CollectionViewModel : BaseViewModel
        {
            public UI_CollectionViewModel()
            {
                Items = new ObservableCollection<int>();
            }

            public void Add()
            {
                var item = Items.Count + 1;
                Items.Add(item);
                SelectedItem = item;
            }

            private int _value;

            public int SelectedItem
            {
                get { return _value; }
                set { SetProperty(ref _value, value); }
            }

            public ObservableCollection<int> Items { get; private set; }
        }
    }
}