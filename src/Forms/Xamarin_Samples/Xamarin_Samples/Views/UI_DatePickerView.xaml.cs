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
    public partial class UI_DatePickerView : ContentPage
    {
        private int _controlType;
        private readonly UI_DatePickerViewModel _model = new UI_DatePickerViewModel();

        public UI_DatePickerView()
        {
            InitializeComponent();

            BindingContext = _model;

            dpCustom.NullableDate = null;
        }
       
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _controlType = 1;
            datePickerStealth.Date = _model.Birthday ?? DateTime.Now;
            datePickerStealth.Focus();
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            _model.Birthday = null;
        }

        private void tapModifiedDate_Tapped(object sender, EventArgs e)
        {
            _controlType = 2;
            datePickerStealth.Date = _model.ModifiedDate ?? DateTime.Now;
            datePickerStealth.Focus();
        }

        private void btnClearModifiedDate_Clicked(object sender, EventArgs e)
        {
            _model.ModifiedDate = null;
        }

        private void datePickerStealth_Unfocused(object sender, FocusEventArgs e)
        {
            if (_controlType == 1)
            {
                _model.Birthday = datePickerStealth.Date;
            }
            if (_controlType == 2)
            {
                _model.ModifiedDate = datePickerStealth.Date;
            }
        }
    }

    public class UI_DatePickerViewModel : BaseViewModel
    {
        public UI_DatePickerViewModel()
        {

        }

        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value); }
        }

        private DateTime? _modifiedDate;
        public DateTime? ModifiedDate
        {
            get { return _modifiedDate; }
            set { SetProperty(ref _modifiedDate, value); }
        }
    }
}