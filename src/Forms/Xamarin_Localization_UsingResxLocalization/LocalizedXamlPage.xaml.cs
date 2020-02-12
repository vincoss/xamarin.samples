using System;
using Xamarin.Forms;

namespace UsingResxLocalization
{
    public partial class LocalizedXamlPage : ContentPage
    {
        public LocalizedXamlPage()
        {
            InitializeComponent();

            var text = string.Join(Environment.NewLine, CultureApp.Cache);
            editorCultures.Text = text;
        }
    }
}