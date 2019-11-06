﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ToolbarItemView : ContentPage
    {
        public UI_ToolbarItemView()
        {
            InitializeComponent();
        }

        private void toolSave_Clicked(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            lblInfo.Text = $"You clicked the \"{item.Text}\" toolbar item.";
        }
    }
}