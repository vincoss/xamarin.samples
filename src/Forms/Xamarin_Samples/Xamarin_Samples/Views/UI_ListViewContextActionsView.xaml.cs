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
    public partial class UI_ListViewContextActionsView : ContentPage
    {
        public UI_ListViewContextActionsView()
        {
            InitializeComponent();

            BindingContext = new UI_ListViewContextActionsViewModel();

        }
        private void meActivateDeactivate_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            lblInfo.Text = item.Text;
        }

        public class UI_ListViewContextActionsViewModel
        {
            public UI_ListViewContextActionsViewModel()
            {
                ItemsSource = new[]
                {
                new ItemInfo {Name = "A"},
                new ItemInfo {Name = "B", IsValid = true}
            };

            }

            public IEnumerable<ItemInfo> ItemsSource { get; set; }
        }



        public class ItemInfo
        {
            public string Name { get; set; }
            public bool IsValid { get; set; }
        }


    }
}