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
            entryA.Focus();
        }
    }

    public class EditorViewModel : BaseViewModel
    {
        private string _a;
        public string A
        {
            get { return _a; }
            set { SetProperty(ref _a, value); }
        }
    }

}