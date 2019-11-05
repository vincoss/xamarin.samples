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
            pages.Add(new AppInformationView());
            pages.Add(new BrowserView());
            pages.Add(new BuiltInStylesSampleView());
            pages.Add(new CheckBoxSampleView());
            pages.Add(new CustomStylesSampleView());
            pages.Add(new DeviceDisplayInformationView());
            pages.Add(new DeviceInfoView());
            pages.Add(new EditorSampleView());
            pages.Add(new EmailView());
            pages.Add(new EntrySampleView());
            pages.Add(new EssentialsShareView());
            pages.Add(new EssentialsSmsView());
            pages.Add(new EssentialsTextToSpeechView());
            pages.Add(new EssentialsVersionTrackingView());
            pages.Add(new FileSystemView());
            pages.Add(new FontsSampleView());
            pages.Add(new GeocodingView());
            pages.Add(new GeolocationView());
            pages.Add(new HyperlinkLabelSampleView());
            pages.Add(new ImageButtonSampleView());
            pages.Add(new ImageSamplesView());
            pages.Add(new LabelSampleView());
            pages.Add(new LauncherView());
            pages.Add(new MapView());
            pages.Add(new NavigationSampleView());
            pages.Add(new PhoneDialerView());
            pages.Add(new PreferencesView());
            pages.Add(new SearchBarView());
            pages.Add(new SecureStorageView());
            pages.Add(new SendEmailSampleView());

            pages.Add(new UI_ListViewGroupingView());
            pages.Add(new UI_ListViewInfiniteScrollView());
            pages.Add(new UI_ListViewInteractivityView());
            pages.Add(new UI_ListViewView());
            pages.Add(new UI_NavigationToolbarView());
            pages.Add(new UI_PickerView());
            pages.Add(new UI_TableViewView());
            pages.Add(new UI_ToolbarItemView());

            pages.Add(new UseFontAwesomeSampleView());
        
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