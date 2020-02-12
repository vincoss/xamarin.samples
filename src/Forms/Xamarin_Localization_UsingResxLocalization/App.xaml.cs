using System.Globalization;
using System.Threading;
using Xamarin_Localization_UsingResxLocalization.Resx;
using Xamarin.Forms;

namespace UsingResxLocalization
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            EnsureCulture();

            MainPage = new NavigationPage(new MainPage());
        }

        public void EnsureCulture()
        {
            CultureApp.Cache.Add("App starting...");
            CultureApp.Cache.Add($"CurrentCulture: {CultureInfo.CurrentCulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");


            CultureApp.Cache.Add("Change App culture...");

            var ci = new CultureInfo("ja");
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            AppResources.Culture = ci; // set the RESX for resource localization

            CultureApp.Cache.Add($"CurrentCulture: {CultureInfo.CurrentCulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");
            CultureApp.Cache.Add($"CurrentUICulture: {CultureInfo.CurrentUICulture}");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
