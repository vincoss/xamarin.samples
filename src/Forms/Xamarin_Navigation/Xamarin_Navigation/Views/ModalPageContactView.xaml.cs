﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Navigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalPageContactView : ContentPage
    {
        public ModalPageContactView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}