﻿using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin_Samples.Views;

namespace Xamarin_Samples
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {
            InitializeComponent();

            VersionTracking.Track();
            MainPage = new NavigationPage(new HomeView());
        }

        protected override void OnStart()
        {
            ListAllEmbeddedResourceNamesDevOnly();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Troubleshooting Embedded Images
        /// </summary>
        private void ListAllEmbeddedResourceNamesDevOnly()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine($"Found resource: {res}");
            }
        }
    }
}
