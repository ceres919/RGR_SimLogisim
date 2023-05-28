using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using ReactiveUI;
using SimLogisim.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using SimLogisim.Models.LoadAndSave;
using DynamicData.Binding;
using SimLogisim.Models.LogicalElements;
using System.Xml.Linq;
using Avalonia.Interactivity;
using Avalonia;
using System.Collections.Generic;

namespace SimLogisim.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string progectName, currentCircuitName, progectStatus;
        private ProjectEntity currentProject;
        private CircuitEntity selectedCircuit;
        private ObservableCollection<CircuitEntity> circuitsCollection;
        private ObservableCollection<IShape> elementsCollection;

        public MainWindowViewModel() { }
        public MainWindowViewModel(ProjectEntity project, string status) 
        {
            progectStatus = status;
            currentProject = project;
            ProjectName = CurrentProject.Name;
            CircuitsCollection = CurrentProject.Circuits;
            SelectedCircuit = CircuitsCollection[0];
            ElementsCollection = SelectedCircuit.Elemets;
        }
        public void AddNewElement(string parameter)
        {
            switch (parameter)
            {
                case "enter":
                    ElementsCollection.Add(new ElementENTER()
                        {
                        Name = "Enter",
                        StartPoint = new Point(10, 10),
                        Exits = new ObservableCollection<int>() { 0 },
                        ValueFill = "WhiteSmoke",
                    });
                    break;
                case "exit":
                    ElementsCollection.Add(new ElementEXIT()
                    {
                        Name = "Exit",
                        StartPoint = new Point(10, 10),
                        Enters = new ObservableCollection<int>() { 0 },
                        ValueFill = "WhiteSmoke",
                    });
                    break;
                case "and":
                    ElementsCollection.Add(new ElementAND()
                    {
                        Name = "And",
                        Exits = new ObservableCollection<int>() { 0 },
                        Enters = new ObservableCollection<int>() { 0, 0 },
                        StartPoint = new Point(10, 10),
                    });
                    break;
                case "or":
                    ElementsCollection.Add(new ElementOR()
                    {
                        Name = "Or",
                        Exits = new ObservableCollection<int>() { 0 },
                        Enters = new ObservableCollection<int>() { 0, 0 },
                        StartPoint = new Point(10, 10),
                    });
                    break;
                case "not":
                    ElementsCollection.Add(new ElementNOT()
                    {
                        Name = "Not",
                        Exits = new ObservableCollection<int>() { 0 },
                        Enters = new ObservableCollection<int>() { 0 },
                        StartPoint = new Point(10, 10),
                    });
                    break;
                case "xor":
                    ElementsCollection.Add(new ElementXOR()
                    {
                        Name = "Xor",
                        Exits = new ObservableCollection<int>() { 0 },
                        Enters = new ObservableCollection<int>() { 0, 0 },
                        StartPoint = new Point(10, 10),
                    });
                    break;
                case "sum":
                    ElementsCollection.Add(new ElementSUM()
                    {
                        Name = "Sum",
                        Exits = new ObservableCollection<int>() { 0, 0 },
                        Enters = new ObservableCollection<int>() { 0, 0, 0 },
                        StartPoint = new Point(10, 10),
                    });
                    break;
                default:
                    break;
            }
        }
        public void AddNewCircuit()
        {
            CircuitsCollection.Add(new CircuitEntity { Name = "New_Circuit", Elemets = new ObservableCollection<IShape>(), });
        }
        public void DeleteCircuitButton(CircuitEntity circuit)
        {
            if(CircuitsCollection.Count > 1)
            {
                CircuitsCollection.Remove(circuit);
                SelectedCircuit = CircuitsCollection[0];
            }
                

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
                if(value != null)
                {
                    CurrentCircuitName = selectedCircuit.Name;
                    ElementsCollection = selectedCircuit.Elemets;
                }
                
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
        public ObservableCollection<IShape> ElementsCollection
        {
            get => elementsCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref elementsCollection, value);
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