using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using SimLogisim.Models;
using SimLogisim.Models.LoadAndSave;
using SimLogisim.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SimLogisim.Views
{
    public partial class LaunchWindow : Window
    {   
        public LaunchWindow()
        {
            InitializeComponent();
            DataContext = new LaunchWindowViewModel();
        }
        private void LoadExistingProject(object sender, RoutedEventArgs routedEventArgs)
        {
            if (DataContext is LaunchWindowViewModel dataContext)
            {
                MainWindow view = new MainWindow(dataContext.LoadProject(), "old");
                view.Show();
                this.Close();
            }
        }
        private void CreateNewProject(object sender, RoutedEventArgs routedEventArgs)
        {
            ProjectEntity project = new ProjectEntity()
            {
                Name = "New_Project",
                DateOfVisit = DateTime.Now,
                Circuits = new ObservableCollection<CircuitEntity>()
                    {
                        new CircuitEntity() {
                            Name = "Circuit1",
                            Elemets = new ObservableCollection<IShape> () {}
                    }, },
            };
            MainWindow view = new MainWindow(project, "new");
            view.Show();
            this.Close();
        }
        public async void OpenFileDialogMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is LaunchWindowViewModel dataContext)
            {
                MainWindow view = new MainWindow(dataContext.LoadProject(result[0]), "old");
                view.Show();
                this.Close();
            }
        }
        private void ExitProgramm(object sender, RoutedEventArgs routedEventArgs)
        {
            System.Environment.Exit(0);
        }
        
    }
}
