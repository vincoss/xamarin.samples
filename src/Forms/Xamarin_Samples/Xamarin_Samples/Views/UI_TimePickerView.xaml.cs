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
    public partial class UI_TimePickerView : ContentPage
    {
        public UI_TimePickerView()
        {
            InitializeComponent();

            var model = new UI_TimePickerViewModel();
            BindingContext = model;
        }
    }

    public class UI_TimePickerViewModel : BaseViewModel
    {
        DateTime _selectedTime = DateTime.Now;
        public DateTime SelectedTime
        {
            get { return _selectedTime; }
            set { SetProperty(ref _selectedTime, value); }
        }
    }
}