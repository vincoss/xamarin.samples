using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;
using static Xamarin_Samples.Views.UI_ListViewCheckBoxViewModel;

namespace Xamarin_Samples.Views
{

    /*
     NOTE: Use DataTemplateSelector to show checkboxex if need multi selection
    */

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ListViewCheckBoxView : ContentPage
    {
        public UI_ListViewCheckBoxView()
        {
            InitializeComponent();

            var model = new UI_ListViewCheckBoxViewModel();
            BindingContext = model;

        }

        private void chAll_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var model = BindingContext as UI_ListViewCheckBoxViewModel;
            if(model == null)
            {
                return;
            }
            for(int i = 0; i < model.ItemsSource.Count; i++)
            {
                var o = model.ItemsSource[i];
                model.ItemsSource[i] = new NameItem { Name = o.Name, IsSelected = chAll.IsChecked };
            }
            model.OnPropertyChanged("ItemsSource");
        }
    }

    public class UI_ListViewCheckBoxViewModel : BaseViewModel
    {
        public UI_ListViewCheckBoxViewModel()
        {
            var items = new[]
            {
               new NameItem { Name = "A"},
               new NameItem { Name = "B"},
               new NameItem { Name = "C", IsSelected = true},
            };

            ItemsSource = new ObservableCollection<NameItem>(items);
        }

        public ObservableCollection<NameItem> ItemsSource { get; set; }

        public class NameItem
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }

    }

    public class CheckBoxOptionSelector : DataTemplateSelector
    {
        public DataTemplate TemplateDefault { get; set; }
        public DataTemplate TemplateClicked { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item != null && (NameItem)item != null)
                return ((NameItem)item).IsSelected ? TemplateClicked : TemplateDefault;
            return TemplateClicked;
        }
    }
}