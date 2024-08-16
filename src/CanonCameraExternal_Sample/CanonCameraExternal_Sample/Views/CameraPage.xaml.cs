using CanonCameraExternal_Lib;
using CanonCameraExternal_Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CanonCameraExternal_Sample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        private readonly CameraPageViewModel _viewModel;

        public CameraPage()
        {
            InitializeComponent();

            var cts = CancellationTokenSource.CreateLinkedTokenSource(CancellationToken.None);
            var service = new CanonEosCameraService();

            _viewModel = new CameraPageViewModel(service, cts);

            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.Initialize();
        }
    }
}