using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ListViewDataTemplateSelectorView : ContentPage
    {
        public UI_ListViewDataTemplateSelectorView()
        {
            InitializeComponent();

            BindingContext = new UI_ListViewDataTemplateSelectorViewModel();
        }

        public class UI_ListViewDataTemplateSelectorViewModel : BaseViewModel
        {
            public UI_ListViewDataTemplateSelectorViewModel()
            {
                ItemsSource = new ObservableCollection<ItemInfo>();

                ItemsSource.Add(new ItemInfo { Name = "A", Comment = "Comment A" });
                ItemsSource.Add(new ItemInfo { Name = "B", Comment = "Comment B", IsValid = true });

                UpdateCommand = new Command(OnUpdate);
            }

            private void OnUpdate()
            {
                var item = ItemsSource.FirstOrDefault();
                if(item == null)
                {
                    return;
                }
                item.IsValid = !item.IsValid;
                var index = ItemsSource.IndexOf(item);
                var u = new ItemInfo { Name = item.Name, Comment = item.Comment, IsValid = item.IsValid };
                ItemsSource.Insert(index, u);
            }

            public ICommand UpdateCommand { get; private set; }

            public ObservableCollection<ItemInfo> ItemsSource { get; private set; }
        }

        public class ItemInfo
        { 
            public string Name { get; set; }
            public string Comment { get; set; }

            public bool IsValid { get; set; }
        }

    }

    public class ItemInfoDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }

        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var itemInfo = item as UI_ListViewDataTemplateSelectorView.ItemInfo;

            return itemInfo.IsValid ? ValidTemplate : InvalidTemplate;
        }
    }
}