
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.TableViewSamples;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_TableViewView : TabbedPage
    {
        public UI_TableViewView()
        {
            Children.Add(new DataIntentXaml());
            Children.Add(new FormIntentXaml());
            Children.Add(new MenuIntentXaml());
            Children.Add(new SettingsIntentXaml());
            Children.Add(new SwitchCellDemoXaml());
            Children.Add(new EntryCellDemoXaml());
        }
    }
}