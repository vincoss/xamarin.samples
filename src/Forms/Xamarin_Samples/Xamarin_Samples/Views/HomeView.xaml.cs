using System.Collections.Generic;

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
            pages.Add(new Essentials_AppInformationView());
            pages.Add(new Essentials_BrowserView());
            pages.Add(new Essentials_DeviceDisplayInformationView());
            pages.Add(new Essentials_DeviceInfoView());
            pages.Add(new Essentials_EmailView());
            pages.Add(new EssentialsShareView());
            pages.Add(new EssentialsSmsView());
            pages.Add(new EssentialsTextToSpeechView());
            pages.Add(new EssentialsVersionTrackingView());
            pages.Add(new ExitAppView());
            pages.Add(new Essentials_FileSystemView());
            pages.Add(new Essentials_GeocodingView());
            pages.Add(new Essentials_GeolocationView());
            pages.Add(new Essentials_LauncherView());
            pages.Add(new Essentials_MapView());
            pages.Add(new Essentials_PhoneDialerView());
            pages.Add(new Essentials_PreferencesView());
            pages.Add(new Essentials_SecureStorageView());
            pages.Add(new Essentials_SendEmailSampleView());

            pages.Add(new UI_BuiltInStylesSampleView());
            pages.Add(new UI_ButtonBottomRightView());
            pages.Add(new UI_CheckBoxSampleView());
            pages.Add(new UI_CustomStylesSampleView());
            pages.Add(new UI_EditorSampleView());
            pages.Add(new UI_EntrySampleView());
            pages.Add(new UI_FontsSampleView());
            pages.Add(new UI_HyperlinkLabelSampleView());
            pages.Add(new UI_ImageButtonSampleView());
            pages.Add(new UI_ImageSamplesView());
            pages.Add(new UI_LabelSampleView());
            pages.Add(new UI_ListViewButtonBottomRightView());
            pages.Add(new UI_ListViewCheckBoxView());
            pages.Add(new UI_ListViewContextActionsView());
            pages.Add(new UI_ListViewDataTemplateSelectorView());
            pages.Add(new UI_ListViewGroupingView());
            pages.Add(new UI_ListViewInfiniteScrollView());
            pages.Add(new UI_ListViewInteractivityView());
            pages.Add(new UI_ListViewView());
            pages.Add(new UI_NavigationToolbarView());
            pages.Add(new UI_PickerView());
            pages.Add(new UI_SearchBarView());
            pages.Add(new UI_TableViewView());
            pages.Add(new UI_ToolbarItemView());
            pages.Add(new UI_UseFontAwesomeSampleView());

            pages.Add(new FlexLayoutHolyGrailView());
            pages.Add(new UI_FlexLayoutView());

            pages.Add(new UI_TriggersView());
            pages.Add(new UI_ControlTemplateView());

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