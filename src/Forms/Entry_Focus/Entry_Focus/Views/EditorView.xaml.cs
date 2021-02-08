using Entry_Focus_CursorPosition.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Entry_Focus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorView : ContentPage
    {
        public EditorView()
        {
            InitializeComponent();

            this.Appearing += EditorView_Appearing;
        }

        private void EditorView_Appearing(object sender, EventArgs e)
        {
            /*
            New show focus and keyboard
            Edit focus no keyboard
             */

            var model = this.BindingContext as EditorViewModel;
            if(model != null && model.IsNew)
            {
                entryA.Focus();
            }
            else
            {
                entryA.CursorPosition = 11111;
            }
        }
    }

    public class EditorViewModel : BaseViewModel
    {
        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set { SetProperty(ref _isNew, value); }
        }

        private string _a;
        public string A
        {
            get { return _a; }
            set { SetProperty(ref _a, value); }
        }
    }

}