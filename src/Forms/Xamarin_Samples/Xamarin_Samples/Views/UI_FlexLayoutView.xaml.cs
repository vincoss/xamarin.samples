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
    public partial class UI_FlexLayoutView : ContentPage
    {
        public UI_FlexLayoutView()
        {
            InitializeComponent();

            BindingContext = new UI_FlexLayoutViewModel();
        }

        public class UI_FlexLayoutViewModel : BaseViewModel
        {
            public UI_FlexLayoutViewModel()
            {
                Colors = new[] { "#bada55", "#7fe5f0", "#ff0000", "#ff80ed", "#696969", "#133337", "#065535", "#DCDCDC", "#ffc454", "#ABCDF3" };
            }

            public IEnumerable<string> Colors { get; set; }
        }

    }
}