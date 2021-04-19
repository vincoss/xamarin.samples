using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetGenericHost.Interfaces;
using NetGenericHost.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace NetGenericHost
{
    public static class BootstrapExtensions
    {
        public static readonly string Namespace = typeof(BootstrapExtensions).Namespace;
        public static readonly string AppName = Namespace;

        public static int Run(string[] args, Action<HostBuilderContext, IServiceCollection> nativeConfigureServices = null)
        {
            //  Log.Logger = CreateSerilogLogger(configuration); // TODO: logging

            try
            {
                // Log.Information("Configuring host ({ApplicationContext})...", AppName);

                var host = CreateHostBuilder(args, nativeConfigureServices).Build();
                App.ServiceProvider = host.Services;

                return 0;
            }
            catch (Exception ex)
            {
                // Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                // Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var configuration = GetConfiguration(args);
            var builder = new HostBuilder()
                .ConfigureHostConfiguration((cfg) =>
                {
                    cfg.AddConfiguration(configuration);
                })
                .ConfigureAppConfiguration((hostingContext, cfg) =>
                {
                    cfg.AddConfiguration(configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(hostContext, services);
                    nativeConfigureServices(hostContext, services);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            return builder;
        }

        private static IConfiguration GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(FileSystem.AppDataDirectory)
                        .AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" })
                        .AddJsonFile(new EmbeddedFileProvider(typeof(BootstrapExtensions).Assembly), "appsettings.json", false, false)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);

            return builder.Build();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddSingleton<IBootstrap, Bootstrap>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<App>();
        }
    }
}
