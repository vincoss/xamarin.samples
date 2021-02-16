using SqliteDapperSample.Database;
using SqliteDapperSample.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SqliteDapperSample.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        public ProductListViewModel(IProductService productService)
        {
            _productService = productService;

            ItemsSource = new ObservableCollection<Product>();

            RefreshCommand = new Command(OnRefreshCommand);
            AddCommand = new Command(OnAddCommand);
        }

        public async void Initialize()
        {
            ItemsSource.Clear();
            DatabasePath = DatabaseConfig.DbPath;
            var products = await _productService.Get();
            
            foreach(var p in products)
            {
                ItemsSource.Add(p);
            }
        }

        private void OnRefreshCommand()
        {
            Initialize();
        }

        private async void OnAddCommand()
        {
            if(string.IsNullOrWhiteSpace(ProductName))
            {
                return;
            }

            var product = new Product
            {
                Name = ProductName
            };
            await _productService.Create(product);
            ProductName = null;
        }

        public ObservableCollection<Product> ItemsSource { get; set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private string _databasePath;

        public string DatabasePath
        {
            get { return _databasePath; }
            set { SetProperty(ref _databasePath, value); }
        }
    }
}
