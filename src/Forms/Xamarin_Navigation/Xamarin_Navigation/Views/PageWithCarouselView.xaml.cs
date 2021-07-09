using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Navigation.ViewModels;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageWithCarouselView : ContentPage
    {
        public PageWithCarouselView()
        {
            InitializeComponent();
            var model = new PageWithCarouselViewModel();
            BindingContext = model;
            model.Initialize();
        }
    }

    public class PageWithCarouselViewModel : BaseViewModel
    { 
        public PageWithCarouselViewModel()
        {
            RefreshCommand = new Command(Initialize);
            Items = new ObservableCollection<ItemInfo>();
            BoardTitle = "Welcome Board!!!";
        }

        public override void Initialize()
        {
            try
            {
                IsBusy = true;
                var bag = new ItemInfo[]
                {
                    new ItemInfo { Title = "Backlog", Count = 2, },
                    new ItemInfo {   Title = "To Do", Count = 0, },
                    new ItemInfo {  Title = "Blocked", Count = 3, },
                    new ItemInfo {  Title = "In Progress", Count = 0, },
                    new ItemInfo {  Title = "Review", Count = 50 },
                    new ItemInfo {  Title = "Done", Count = 1000 },
                };
                Items.Clear();
                foreach(var item in bag)
                {
                    Items.Add(item);
                }

                Position = 2;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand RefreshCommand { get; private set; }

        public string BoardTitle { get; private set; }

        public ObservableCollection<ItemInfo> Items { get; private set; }

        private ItemInfo _selectedItem;

        public ItemInfo SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private int _position;

        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public class ItemInfo
        {
            public string Title { get; set; }
            public int Count { get; set; }
        }
    }

}