using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefreshViewSampleView : ContentPage
    {
        public RefreshViewSampleView()
        {
            InitializeComponent();

            BindingContext = new RefreshViewSampleViewModel();
        }

        public class RefreshViewSampleViewModel : BaseViewModel
        {
            public RefreshViewSampleViewModel()
            {
                RefreshCommand = new Command(OnRefreshCommand);
                LoadItems();
            }

            private async void LoadItems()
            {
                try
                {
                    IsBusy = true;
                    Items = Enumerable.Range(0, 1000);
                    await Task.Delay(1000);
                }
                finally
                {
                    IsBusy = false;
                }
            }

            private void OnRefreshCommand()
            {
                LoadItems();
            }

            public IEnumerable<int> Items { get; private set; }

            public ICommand RefreshCommand { get; private set; }
        }
    }
}