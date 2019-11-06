
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_NavigationToolbarView : ContentPage
    {
        public UI_NavigationToolbarView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.Red;

            base.OnAppearing();
        }
    }
}