﻿using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Samples.Views;

namespace Xamarin_Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FontsSampleView();
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