
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_SearchBarView : ContentPage
    {
        public UI_SearchBarView()
        {
            InitializeComponent();

            BindingContext = new SearchBarViewModel();
        }
    }
}