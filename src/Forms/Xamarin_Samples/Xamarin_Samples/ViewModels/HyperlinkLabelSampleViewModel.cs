using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamarin_Samples.ViewModels
{
    public class HyperlinkLabelSampleViewModel
    {

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Launcher.OpenAsync(new System.Uri(url));
        });
    }
}
