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
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            List<Page> pages = new List<Page>();
            pages.Add(new UI_PickerView());
            pages.Add(new UI_TableViewView());
            pages.Add(new UI_ToolbarItemView());
            pages.Add(new UI_NavigationToolbarView());
            pages.Add(new UI_ListViewView());
            pages.Add(new UI_ListViewInteractivityView());
            pages.Add(new UI_ListViewInfiniteScrollView());
            pages.Add(new UI_ListViewGroupingView());
            pages.Add(new UseFontAwesomeSampleView());
            pages.Add(new SendEmailSampleView());
            pages.Add(new SecureStorageView());
            pages.Add(new SearchBarView());

            ListOfPages.ItemsSource = pages;
        }

        async void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var page = (Page)e.SelectedItem;

                await this.Navigation.PushAsync((Page)e.SelectedItem);
            }
            ListOfPages.SelectedItem = null;
        }
    }
}