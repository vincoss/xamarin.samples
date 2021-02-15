using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UI_CollectionViewGrouping : ContentPage
    {
        public UI_CollectionViewGrouping()
        {
            InitializeComponent();

            BindingContext = new UI_CollectionViewGroupingModel();
        }
    }

    public class Project
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ProjectGroup : List<Project>
    {
        public string Name { get; private set; }

        public ProjectGroup(string name, List<Project> projects) : base(projects)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }


    public class UI_CollectionViewGroupingModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProjectGroup> Projects { get; private set; } = new ObservableCollection<ProjectGroup>();

        public UI_CollectionViewGroupingModel()
        {
            SampleData();
        }

        private void SampleData()
        {
            var a = new[]
            {
            new Project {Name = "atherosclerosis"},
            new Project {Name = "autocorrelation"}
        };

            var b = new[]
            {
            new Project { Name = "bioavailability"},
            new Project { Name = "biogeochemistry"},
        };

            Projects.Add(new ProjectGroup("A", a.ToList()));
            Projects.Add(new ProjectGroup("B", b.ToList()));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}