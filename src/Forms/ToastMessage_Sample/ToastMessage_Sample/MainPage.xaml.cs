using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToastMessage_Sample.Interface;
using Xamarin.Forms;

namespace ToastMessage_Sample
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _model = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = _model;
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

        private void Button_ClickedCustom(object sender, EventArgs e)
        {
            _model.ToastMessage = "I'm custom alert.";
        }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _toastMessage;
        public string ToastMessage
        {
            get { return _toastMessage; }
            set
            {
                _toastMessage = value;
                OnPropertyChanged(nameof(ToastMessage));
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
