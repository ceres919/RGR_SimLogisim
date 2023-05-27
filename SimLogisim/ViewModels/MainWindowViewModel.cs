using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using ReactiveUI;
using SimLogisim.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using SimLogisim.Models.LoadAndSave;
using DynamicData.Binding;

namespace SimLogisim.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string progectName, currentCircuitName, progectStatus;
        private ProjectEntity currentProject;
        private CircuitEntity selectedCircuit;
        private ObservableCollection<CircuitEntity> circuitsCollection;
        public MainWindowViewModel() { }
        public MainWindowViewModel(ProjectEntity project, string status) 
        {
            progectStatus = status;
            currentProject = project;
            ProjectName = project.Name;
            CircuitsCollection = project.Circuits;
            SelectedCircuit = CircuitsCollection[0];

        }
        public void AddNewCircuit()
        {
            CircuitsCollection.Add(new CircuitEntity { Name = "New_Circuit", Elemets = new ObservableCollection<IShape>(), });
        }
        public void SaveCurrentProject(string path)
        {
            JSONSaver json_saver = new JSONSaver();
            XMLLoader xml_loader = new XMLLoader();
            ObservableCollection<ProjectEntity> projects = new ObservableCollection<ProjectEntity>(xml_loader.Load("../../../ProjectsStorage.xml"));

            switch (progectStatus) 
            {
                case "old":
                    projects[0].Name = ProjectName;
                    projects[0].FileName = path;
                    break;
                case "new":
                    projects.Insert(0, CurrentProject);
                    break;
                default: break;
            }
            CurrentProject.FileName = path;
            json_saver.Save(CurrentProject, path);
            XMLSaver xml_saver = new XMLSaver();
            xml_saver.Save(projects, "../../../ProjectsStorage.xml");
        }
        public ProjectEntity CurrentProject
        {
            get => currentProject;
            set
            {
                this.RaiseAndSetIfChanged(ref currentProject, value);
            }
        }
        public CircuitEntity SelectedCircuit
        {
            get => selectedCircuit;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedCircuit, value);
                CurrentCircuitName = value.Name;
            }
        }
        public ObservableCollection<CircuitEntity> CircuitsCollection
        {
            get => circuitsCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref circuitsCollection, value);
            }
        }
        public string ProjectName
        {
            get => progectName;
            set 
            {
                this.RaiseAndSetIfChanged(ref progectName, value);
                CurrentProject.Name = value;
            }
        }
        public string CurrentCircuitName
        {
            get => currentCircuitName;
            set
            {
                this.RaiseAndSetIfChanged(ref currentCircuitName, value);
                SelectedCircuit.Name = value;
            }
        }
    }
}