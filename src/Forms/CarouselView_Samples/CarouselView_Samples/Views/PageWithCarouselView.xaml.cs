using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarouselView_Samples.ViewModels;

namespace CarouselView_Samples.Views
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
            Items = new ObservableCollection<ColumnDto>();
            BoardTitle = "Welcome Board!!!";
        }

        public override void Initialize()
        {
            try
            {
                IsBusy = true;
                var bag = new ColumnDto[]
                {
                    new ColumnDto
                    {
                        Name = "Backlog"
                    },
                    new ColumnDto {  Name = "To Do" },
                    new ColumnDto {  Name = "Blocked" },
                    new ColumnDto {  Name = "In Progress" },
                    new ColumnDto {  Name = "Review" },
                    new ColumnDto {  Name = "Done" },
                };

                bag[0].Cards.Add(new CardListItemDto { Name = "A" });
                bag[0].Cards.Add(new CardListItemDto { Name = "B" });
                bag[0].Cards.Add(new CardListItemDto { Name = "C" });

                bag[2].Cards.Add(new CardListItemDto { Name = "A1" });

                Items.Clear();
                foreach (var item in bag)
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

        public ObservableCollection<ColumnDto> Items { get; private set; }

        private ColumnDto _selectedItem;

        public ColumnDto SelectedItem
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

        public class ColumnDto
        {
            public ColumnDto()
            {
                Cards = new ObservableCollection<CardListItemDto>();
            }

            public string Name { get; set; }

            public int Count
            {
                get { return Cards.Count; }
            }

            public ObservableCollection<CardListItemDto> Cards { get; private set; }
        }

        public class CardListItemDto
        {
            public string Name { get; set; }

            public override string ToString()
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return base.ToString();
                }
                return Name;
            }
        }
    }
}