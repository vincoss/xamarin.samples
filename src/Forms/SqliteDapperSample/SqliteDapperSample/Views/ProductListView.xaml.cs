using SqliteDapperSample.Database;
using SqliteDapperSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqliteDapperSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListView : ContentPage
    {
        private ProductListViewModel model = new ProductListViewModel(new ProductService(new DatabaseConfig()));

        public ProductListView()
        {
            InitializeComponent();

            BindingContext = model;
        }
    }
}