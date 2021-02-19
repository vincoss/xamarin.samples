using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamarin_Samples.ViewModels
{
    public class LabelSampleViewModel
    {
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Launcher.OpenAsync(new System.Uri(url));
        });

        public string FormatedLabelString
        {
            get { return "Jobs"; }
        }

        public string FirstName
        {
            get { return "Ferdinand"; }
        }
    }
}
