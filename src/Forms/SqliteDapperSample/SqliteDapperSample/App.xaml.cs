using SqliteDapperSample.Database;
using SqliteDapperSample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqliteDapperSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductListView());
        }

        protected override void OnStart()
        {
            var config = new DatabaseConfig();
            var bootstrap = new DatabaseBootstrap(config);
            bootstrap.Run();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
