using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageOneView : ContentPage
    {
        public PageOneView(DateTime date)
        {
            InitializeComponent();

            // Passing Data when Navigating
            lblDate.Text = date.ToString();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            NavigationPage.SetBackButtonTitle(this, "Back to Main Page");
            await Navigation.PushAsync(new PageTwoView());
        }

        private async void btnTabbedPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPageOneView());
        }

        private async void btnTabbedPageTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPageTemplateView());
        }

        private async void btnCarouselPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarouselPageView());
        }

        private async void btnCarouselPageTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarouselPageTemplateView());
        }

        private async void btnMasterDetailPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MasterDetailPageView());
        }

        private async void btnModelPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalPageView());
        }

        private async void btnModelPageDetail_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalPageParentView());
        }
    }
}