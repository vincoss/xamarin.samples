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
    public partial class NavigationSampleView : ContentPage
    {
        public NavigationSampleView()
        {
            InitializeComponent();

            List<Page> pages = new List<Page>();
            pages.Add(new BuiltInStylesSampleView());
            pages.Add(new CustomStylesSampleView());
            pages.Add(new EditorSampleView());
            pages.Add(new EntrySampleView());
            pages.Add(new FontsSampleView());
            pages.Add(new HyperlinkLabelSampleView());
            pages.Add(new ImageSamplesView());
            pages.Add(new LabelSampleView());

            ListOfPages.ItemsSource = pages;
        }

        async void itemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await this.Navigation.PushAsync((Page)e.SelectedItem);
            }
            ListOfPages.SelectedItem = null;
        }
    }
}