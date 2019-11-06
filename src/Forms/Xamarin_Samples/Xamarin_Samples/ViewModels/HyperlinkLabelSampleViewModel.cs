using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin_Samples.ViewModels
{
    public class HyperlinkLabelSampleViewModel
    {

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });
    }
}
