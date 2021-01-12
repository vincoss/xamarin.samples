using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Forms_WebView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            List<PageInfo> pages = new List<PageInfo>();
            pages.Add(new PageInfo { Type = typeof(SourceUrlVIew) });
            pages.Add(new PageInfo { Type = typeof(SourceHtmlStringView) });
            pages.Add(new PageInfo { Type = typeof(SourceLocalFileView) });

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