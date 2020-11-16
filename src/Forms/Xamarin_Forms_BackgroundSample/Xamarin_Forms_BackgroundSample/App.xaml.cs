using Matcha.BackgroundService;
using Xamarin.Forms;
using Xamarin_Forms_BackgroundSample.Services;


namespace Xamarin_Forms_BackgroundSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            StartBackgroundService();
        }

        protected override void OnSleep()
        {
            // Stop if required
            //BackgroundAggregatorService.StopBackgroundService(); // TODO:
        }

        protected override void OnResume()
        {
            if (Device.RuntimePlatform == Device.iOS) StartBackgroundService();
        }

        private static void StartBackgroundService()
        {
            // Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new PeriodicService());

            // Start the background service
            BackgroundAggregatorService.StopBackgroundService(); // Fix for error if already running. UWP Cannot create a file when that file already exists.
            BackgroundAggregatorService.StartBackgroundService();
        }
    }
}
