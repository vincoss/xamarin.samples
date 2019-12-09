using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_CollectionView : ContentPage
    {
        public UI_CollectionView()
        {
            InitializeComponent();

            Items = new ObservableCollection<int>();
            scrolToItems.ItemsSource = Items;
        }

        private void addItem_Clicked(object sender, EventArgs e)
        {
            var item = Items.Count + 1;
            Items.Add(item);
        }

        public ObservableCollection<int> Items { get; private set; }
    }
}