using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace NetGenericHost.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            BootstrapExtensions.Run(new string[0], ConfigureServices);
            LoadApplication(NetGenericHost.App.ServiceProvider.GetService<NetGenericHost.App>());
        }

        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // Register here navive calls
        }
    }
}
