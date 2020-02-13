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
    public partial class EN_EnvironmentView : ContentPage
    {
        private readonly IList<string> _environment = new List<string>();
        private readonly IList<string> _logicalDrives = new List<string>();
        private readonly IList<string> _environmentVariables = new List<string>();
        private readonly IList<string> _environmentSpecialFolders = new List<string>();

        public EN_EnvironmentView()
        {
            InitializeComponent();

            LoadEnvironmentValues();
            GetLogicalDrives();
            GetEnvironmentVariables();
            GetFolderPath();

            editorEnvironment.Text = string.Join(Environment.NewLine, _environment);
            editorLogicalDrives.Text = string.Join(Environment.NewLine, _logicalDrives);
            editorVariables.Text = string.Join(Environment.NewLine, _environmentVariables);
            editorSpecialFolders.Text = string.Join(Environment.NewLine, _environmentSpecialFolders);
        }

        public void LoadEnvironmentValues()
        {
            Add(_environment, nameof(Environment.HasShutdownStarted), Environment.HasShutdownStarted);
            Add(_environment, nameof(Environment.MachineName), Environment.MachineName);
            Add(_environment, nameof(Environment.ExitCode), Environment.ExitCode);
            Add(_environment, nameof(Environment.CurrentManagedThreadId), Environment.CurrentManagedThreadId);
            Add(_environment, nameof(Environment.CurrentDirectory), Environment.CurrentDirectory);
            Add(_environment, nameof(Environment.CommandLine), Environment.CommandLine);
            Add(_environment, nameof(Environment.NewLine), Environment.NewLine);
            Add(_environment, nameof(Environment.OSVersion), Environment.OSVersion);
            Add(_environment, nameof(Environment.ProcessorCount), Environment.ProcessorCount);
            Add(_environment, nameof(Environment.Is64BitOperatingSystem), Environment.Is64BitOperatingSystem);
            Add(_environment, nameof(Environment.StackTrace), Environment.StackTrace);
            Add(_environment, nameof(Environment.SystemPageSize), Environment.SystemPageSize);
            Add(_environment, nameof(Environment.TickCount), Environment.TickCount);
            //Add(nameof(Environment.TickCount64), Environment.TickCount64);
            Add(_environment, nameof(Environment.UserDomainName), Environment.UserDomainName);
            Add(_environment, nameof(Environment.UserInteractive), Environment.UserInteractive);
            Add(_environment, nameof(Environment.UserName), Environment.UserName);
            Add(_environment, nameof(Environment.Version), Environment.Version);
            try { Add(_environment, nameof(Environment.WorkingSet), Environment.WorkingSet); } catch (Exception e) { Add(_environment, nameof(Environment.WorkingSet), e.Message); }
            Add(_environment, nameof(Environment.SystemDirectory), Environment.SystemDirectory);
            Add(_environment, nameof(Environment.Is64BitProcess), Environment.Is64BitProcess);
        }

        public void GetLogicalDrives()
        {
            try
            {
                var items = Environment.GetLogicalDrives();
                foreach(var d in items)
                {
                    _logicalDrives.Add(d);
                }
            }
            catch(Exception e)
            {
                _logicalDrives.Add(e.Message);
            }
        }

        public void GetEnvironmentVariables()
        {
            try
            {
                var items = Environment.GetEnvironmentVariables();
                foreach (var k in items.Keys)
                {
                    _environmentVariables.Add($"{k}: {items[k]}");
                }
            }
            catch (Exception e)
            {
                _logicalDrives.Add(e.Message);
            }
        }

        public void GetFolderPath()
        {
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Desktop.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Programs.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Programs));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.MyDocuments.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Personal.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Favorites.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Startup.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Recent.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Recent));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.SendTo.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.StartMenu.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.MyMusic.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.MyVideos.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.DesktopDirectory.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.MyComputer.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyComputer));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.NetworkShortcuts.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Fonts.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Fonts));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Templates.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Templates));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonStartMenu.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonPrograms.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonDesktopDirectory.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.ApplicationData.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.PrinterShortcuts.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.LocalApplicationData.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.InternetCache.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Cookies.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.History.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.History));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonApplicationData.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Windows.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.System.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.System));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.ProgramFiles.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.MyPictures.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.UserProfile.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.SystemX86.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.SystemX86));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.ProgramFilesX86.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonProgramFiles.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonProgramFilesX86.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonTemplates.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonDocuments.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonAdminTools.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.AdminTools.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.AdminTools));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonMusic.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonPictures.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonVideos.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.Resources.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.Resources));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.LocalizedResources.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CommonOemLinks.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CommonOemLinks));
            Add(_environmentSpecialFolders, Environment.SpecialFolder.CDBurning.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.CDBurning));
        }

        public  void Add(IList<string> bag, string key, dynamic value)
        {
            try
            {
                bag.Add($"{key}: {value}");
            }
            catch(Exception e)
            {
                bag.Add($"{key}: {e.Message}");
            }
        }
    }
}