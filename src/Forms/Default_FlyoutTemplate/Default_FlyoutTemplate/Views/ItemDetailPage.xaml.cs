using Default_FlyoutTemplate.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Default_FlyoutTemplate.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}