using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastMessage_Sample.Interface;
using Xamarin.Forms;

namespace ToastMessage_Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_ClickedShort(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IMessage>();
            service.ShortAlert("I'm Short Alert");
        }

        private void Button_ClickedLong(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IMessage>();
            service.LongAlert("I'm Long Alert");
        }
    }
}
