using CanonCameraExternal_Sample_WPF.Services;
using CanonCameraExternal_Sample_WPF.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanonCameraExternal_Sample_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICloseable
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += ShellView_Loaded;
            this.Closing += ShellView_Closing;

            var cts = CancellationTokenSource.CreateLinkedTokenSource(CancellationToken.None);
            var service = new CanonEosCameraService();

            this.DataContext = new ShellViewModel(service, cts);
        }

        private void ShellView_Loaded(object sender, RoutedEventArgs e)
        {
            var model = DataContext as ShellViewModel;
            if (model != null)
            {
                model.Initialize();
            }
        }

        private void ShellView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var model = DataContext as ShellViewModel;
            if (model != null)
            {
                model.Dispose();
            }
        }
    }
}