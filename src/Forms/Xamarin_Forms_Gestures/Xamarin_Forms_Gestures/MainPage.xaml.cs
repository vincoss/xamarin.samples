using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin_Forms_Gestures_Samples
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lblTap.Text = "hi there!!!";
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            lblSwipe.Text = "hi there!!!";
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            string text = item.CommandParameter.ToString();
            lblListView.Text = text;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            string text = e.SelectedItem.ToString();
            lblListView.Text = text;
        }
    }
}
