using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms_EnterpriseApp_Authentication.Services;
using XamarinForms_EnterpriseApp_Authentication.ViewModels;

namespace XamarinForms_EnterpriseApp_Authentication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            var service = new MicrosoftAuthService();
            var viewModel = new HomeViewModel(service);
            viewModel.Initialize();

            BindingContext = viewModel;
        }
    }
}