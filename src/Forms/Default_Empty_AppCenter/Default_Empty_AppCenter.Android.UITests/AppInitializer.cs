﻿using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Default_Empty_AppCenter.Android.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("com.companyname.default_empty_appcenter")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}