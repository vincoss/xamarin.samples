using CollectionView_Samples.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CollectionView_Samples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var pages = new List<PageInfo>();
            pages.Add(new PageInfo { Type = typeof(CollectionViewAddView) });

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
