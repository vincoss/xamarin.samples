﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_FilePicker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilePickerView : ContentPage
    {
        public FilePickerView()
        {
            InitializeComponent();
        }

        private async void btnSelectFile_Clicked(object sender, EventArgs e)
        {
            var fileTypes = new string[] { "JPEG files (*.jpg)|*.jpg", "PNG files (*.png)|*.png" };

            await PickAndShowFile(new string[0]);

        }

        private async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                // Opening the File Picker - Filter with Jpeg image
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select your picture",
                    FileTypes = FilePickerFileType.Jpeg
                });

                if (result != null)
                {
                    lFileName.Text = result.FileName;
                    lFilePath.Text = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                lFileName.Text = ex.ToString();
                lFilePath.Text = string.Empty;
            }
        }
    }
}