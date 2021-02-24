using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_CompiledBindingsView : ContentPage
    {
        public UI_CompiledBindingsView()
        {
            InitializeComponent();

            //var model = new KeyData<int, string>
            //{
            //    Key = 1,
            //    Value = "One"
            //};

            var model = new UI_CompiledBindingsViewModel
            {
                Key = 1,
                Value = "One"
            };

            BindingContext = model;
        }

    }


    public interface IKeyData<K, V>
    {
        K Key { get; set; }
        V Value { get; set; }
    }

    public interface IData : IKeyData<int, string>
    {

    }

    public class KeyData<K, V> : IKeyData<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class UI_CompiledBindingsViewModel : IData
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }

}