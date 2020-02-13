using Serilog;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_SerilogSample.Logging;

namespace Xamarin_SerilogSample
{
    /// <summary>
    /// https://github.com/serilog/serilog/wiki/Writing-Log-Events
    /// </summary>
    public partial class App : Application
    {
        private static InbuiltLogger _logger;

        public App()
        {
            InitializeComponent();

            SetupLog(); 
            _logger = InbuiltLog.For(typeof(App));

            MainPage = new MainPage();
        }

        /// <summary>
        /// Log levels
        /// 1. Verbose
        /// 2. Debug
        /// 3. Information
        /// 4. Warning
        /// 5. Error
        /// 6. Fatal
        /// </summary>
        private void SetupLog()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var logpath = Path.Combine(path, $"{nameof(App)}.txt");

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.File(logpath,
                   rollingInterval: RollingInterval.Day,
                   rollOnFileSizeLimit: true)
               .CreateLogger();

            Log.Information(nameof(SetupLog));

            InbuiltLog.SetFactory(new SerilogLoggerFactory());
        }

        protected override void OnStart()
        {
            Log.Information(nameof(OnStart));

            _logger.Info(nameof(OnStart) + "Inbuilt");
            _logger.Debug(nameof(OnStart) + "Inbuilt");
            _logger.Warning(nameof(OnStart) + "Inbuilt");
            _logger.Error(nameof(OnStart) + "Inbuilt");

            _logger.Debug("Current date: {0}", DateTime.Now);
        }

        protected override void OnSleep()
        {
            Log.Information(nameof(OnSleep));
            Log.Error(nameof(OnSleep));
            Log.Verbose(nameof(OnSleep));
            Log.Warning(nameof(OnSleep));
            Log.Debug(nameof(OnSleep));
            Log.Fatal(nameof(OnSleep));

            Log.CloseAndFlush();
        }

        protected override void OnResume()
        {
            Log.Error(nameof(OnResume));
            Log.Verbose(nameof(OnResume));
            Log.Warning(nameof(OnResume));
            Log.Debug(nameof(OnResume));
            Log.Fatal(nameof(OnResume));
        }
    }
}
