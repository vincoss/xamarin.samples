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
    public partial class UI_CollectionViewMultipleSelection : ContentPage
    {
        public UI_CollectionViewMultipleSelection()
        {
            InitializeComponent();

            BindingContext = new UI_CollectionViewMultipleSelectionModel();
        }
    }

    public class UI_CollectionViewMultipleSelectionModel
    {
        public UI_CollectionViewMultipleSelectionModel()
        {
            var items = new[]
            {
                "abstract", "as", "base", "bool", "break"
            };

            foreach(var k in items)
            {
                ItemsSource.Add(k);
            }
        }

        public ObservableCollection<string> ItemsSource { get; private set; } = new ObservableCollection<string>();

    }
}