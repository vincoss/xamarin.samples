using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.ViewModels;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_PickerKeepSelectionView : ContentPage
    {
        public UI_PickerKeepSelectionView()
        {
            InitializeComponent();

            var model = new UI_PickerKeepSelectionViewModel();
            BindingContext = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var model = BindingContext as UI_PickerKeepSelectionViewModel;
            if(model == null)
            {
                return;
            }
            model.Initialize();
        }
    }

    public class UI_PickerKeepSelectionViewModel : BaseViewModel
    {
        public string MonkeyKey = nameof(SelectedMonkey);

        public UI_PickerKeepSelectionViewModel()
        {
            this.PropertyChanged += UI_PickerKeepSelectionViewModel_PropertyChanged;
        }

        public override void Initialize()
        {
            if(App.Current.Properties.ContainsKey(MonkeyKey))
            {
                SelectedMonkey = (string)App.Current.Properties[MonkeyKey];
            }
        }

        private void UI_PickerKeepSelectionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedMonkey))
            {
                App.Current.Properties.Add(MonkeyKey, SelectedMonkey);

                if(SelectedMonkey == null)
                {
                    App.Current.Properties.Remove(MonkeyKey);
                }
            }
        }

        string _selectedMonkey = string.Empty;

        public string SelectedMonkey
        {
            get { return _selectedMonkey; }
            set { SetProperty(ref _selectedMonkey, value); }
        }
    }
}