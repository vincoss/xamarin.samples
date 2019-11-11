using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.Interfaces;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExitAppView : ContentPage
    {
        public ExitAppView()
        {
            InitializeComponent();
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            var context = DependencyService.Get<IApplicationContext>();
            context.Exit();
        }
    }
}