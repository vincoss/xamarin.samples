using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace NetGenericHost.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            BootstrapExtensions.Run(new string[0], ConfigureServices);
            LoadApplication(App.ServiceProvider.GetService<App>());

            return base.FinishedLaunching(app, options);
        }

        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // Register here navive calls
        }
    }
}
