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
    public partial class UI_SwipeView : ContentPage
    {
        public UI_SwipeView()
        {
            InitializeComponent();
        }

        private async void FavoriteSwipeItem_Invoked(object sender, EventArgs e)
        {
            await this.DisplayAlert("Favorite", "Favorite delete", "OK");
        }

        private async void DeleteSwipeItem_Invoked(object sender, EventArgs e)
        {
           await this.DisplayAlert("Delete", "Swipe delete", "OK");
        }
    }
}