using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_ApplicationDataView : ContentPage
    {
        public UI_ApplicationDataView()
        {
            InitializeComponent();

            var paths = new[]
            {
                Constants.GetRootPath(),
                Constants.GetBackupPath(),
                Constants.GetLogsPath(),
                Constants.GetGaleryPath()
            };

            editorPaths.Text = string.Join(Environment.NewLine, paths);
        }

        class Constants
        {
            public static string GetRootPath()
            {
               return  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            }

            public static string GetBackupPath()
            {
                return Path.Combine(GetRootPath(), "backup");
            }

            public static string GetLogsPath()
            {
                return Path.Combine(GetRootPath(), "logs");
            }

            public static string GetGaleryPath()
            {
                return Path.Combine(GetRootPath(), "galery");
            }
        }

    
    }

    
}