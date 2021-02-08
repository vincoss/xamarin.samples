using Entry_Focus_CursorPosition.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Entry_Focus_CursorPosition
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void buttonNew_Clicked(object sender, EventArgs e)
        {
            var view = new EditorView();
            var model = new EditorViewModel();
            view.BindingContext = model;
            this.Navigation.PushAsync(view);
        }

        private void buttonEdit_Clicked(object sender, EventArgs e)
        {
            var view = new EditorView();
            var model = new EditorViewModel();
            model.A = "A";
            view.BindingContext = model;

            this.Navigation.PushAsync(view);
        }
    }
}
