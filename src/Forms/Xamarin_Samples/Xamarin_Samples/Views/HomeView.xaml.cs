using System;
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

            List<PageInfo> pages = new List<PageInfo>();
            pages.Add(new PageInfo{ Type = typeof(Essentials_AppInformationView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_BrowserView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_DeviceDisplayInformationView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_DeviceInfoView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_EmailView)});
            pages.Add(new PageInfo{ Type = typeof(EssentialsShareView)});
            pages.Add(new PageInfo{ Type = typeof(EssentialsSmsView)});
            pages.Add(new PageInfo{ Type = typeof(EssentialsTextToSpeechView)});
            pages.Add(new PageInfo{ Type = typeof(EssentialsVersionTrackingView)});
            pages.Add(new PageInfo{ Type = typeof(ExitAppView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_FileSystemView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_GeocodingView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_GeolocationView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_LauncherView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_MapView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_PhoneDialerView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_PreferencesView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_SecureStorageView)});
            pages.Add(new PageInfo{ Type = typeof(Essentials_SendEmailSampleView)});

            pages.Add(new PageInfo{ Type = typeof(UI_BuiltInStylesSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ButtonBottomRightView)});
            pages.Add(new PageInfo { Type = typeof(BindableLayoutSampleView) });

            pages.Add(new PageInfo{ Type = typeof(UI_CheckBoxSampleView)});
            pages.Add(new PageInfo { Type = typeof(UI_CollectionView) });
            pages.Add(new PageInfo{ Type = typeof(UI_CustomStylesSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_EditorSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_EntrySampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_FontsSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_HyperlinkLabelSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ImageButtonSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ImageSamplesView)});
            pages.Add(new PageInfo{ Type = typeof(UI_LabelSampleView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewButtonBottomRightView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewCheckBoxView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewContextActionsView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewDataTemplateSelectorView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewGroupingView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewInfiniteScrollView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewInteractivityView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ListViewView)});
            pages.Add(new PageInfo{ Type = typeof(UI_NavigationToolbarView)});
            pages.Add(new PageInfo{ Type = typeof(UI_PickerView)});
            pages.Add(new PageInfo{ Type = typeof(UI_PickerKeepSelectionView)});
            pages.Add(new PageInfo{ Type = typeof(UI_SearchBarView)});
            pages.Add(new PageInfo{ Type = typeof(UI_TableViewView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ToolbarItemView)});
            pages.Add(new PageInfo{ Type = typeof(UI_UseFontAwesomeSampleView)});
            pages.Add(new PageInfo { Type = typeof(UI_PopupView) });

            pages.Add(new PageInfo{ Type = typeof(FlexLayoutHolyGrailView)});
            pages.Add(new PageInfo{ Type = typeof(UI_FlexLayoutView)});

            pages.Add(new PageInfo{ Type = typeof(EN_EnvironmentView)});
            pages.Add(new PageInfo{ Type = typeof(UI_TriggersView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ControlTemplateView)});
            pages.Add(new PageInfo{ Type = typeof(UI_ApplicationDataView)});
            pages.Add(new PageInfo { Type = typeof(UI_RefreshViewSampleView) });
            pages.Add(new PageInfo { Type = typeof(UI_SwipeView) });
            pages.Add(new PageInfo { Type = typeof(UI_NavigationTitleView) });
            pages.Add(new PageInfo { Type = typeof(UI_TableViewHideCell) });
            pages.Add(new PageInfo { Type = typeof(UI_ActivityIndicatorSampleView) });
            pages.Add(new PageInfo { Type = typeof(UI_UrlEntryView) });
            pages.Add(new PageInfo { Type = typeof(UI_CollectionViewGrouping) });
            pages.Add(new PageInfo { Type = typeof(UI_CollectionViewMultipleSelection) });
            pages.Add(new PageInfo { Type = typeof(UI_CompiledBindingsView) });
            pages.Add(new PageInfo { Type = typeof(UI_DatePickerView) });

            ListOfPages.ItemsSource = pages;
        }

        async void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var info = (PageInfo)e.SelectedItem;
                var page = (Page)Activator.CreateInstance(info.Type);

                await this.Navigation.PushAsync(page);
            }
            ListOfPages.SelectedItem = null;
        }

        public class PageInfo
        {
            public Type Type { get; set; }
            public string Name
            {

                get
                {
                    if (Type != null)
                    {
                        return Type.Name;
                    }
                    return base.GetType().ToString();
                }
            }

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