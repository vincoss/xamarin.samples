using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           await  PickAndShowFile(new string[0]);

           // var fileTypes = new string[] { "JPEG files (*.jpg)|*.jpg", "PNG files (*.png)|*.png" };
        }


        private async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                var pickedFile = await CrossFilePicker.Current.PickFile();

                if (pickedFile != null)
                {
                    lFileName.Text = pickedFile.FileName;
                    lFilePath.Text = pickedFile.FilePath;

                   
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