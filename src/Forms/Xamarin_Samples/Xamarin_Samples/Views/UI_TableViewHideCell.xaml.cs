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
    public partial class UI_TableViewHideCell : ContentPage
    {
        private readonly UI_TableViewHideCellModel _model = new UI_TableViewHideCellModel();

        public UI_TableViewHideCell()
        {
            InitializeComponent();

            BindingContext = _model;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _model.IsCellVisisble = !_model.IsCellVisisble;
        }
    }

    public class UI_TableViewHideCellModel : BaseViewModel
    {
        private bool _isCellVisible;

        public bool IsCellVisisble
        {
            get { return _isCellVisible; }
            set { SetProperty(ref _isCellVisible, value); }
        }
    }
}