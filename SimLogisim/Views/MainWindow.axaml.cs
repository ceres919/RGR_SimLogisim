using Avalonia.Controls;
using Avalonia.Interactivity;
using SimLogisim.Models;
using SimLogisim.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using Avalonia.VisualTree;

namespace SimLogisim.Views
{
    public partial class MainWindow : Window
    {
        private Canvas canv;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(ProjectEntity project, string status)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(project, status);
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
            //if (DataContext is MainWindowViewModel dataContext)
            //{
            //    if (result != null)
            //    {
            //        dataContext.LoadProject(result[0]);
            //    }
            //}
        }
        private void ExitProgramm(object sender, RoutedEventArgs routedEventArgs)
        {
            System.Environment.Exit(0);
        }
        public async void SaveFileDialogMenu(object sender, RoutedEventArgs routedEventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
            string? result = await saveFileDialog.ShowAsync(this);

            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    //canv = this.GetVisualDescendants().OfType<Canvas>().FirstOrDefault();
                    dataContext.SaveCurrentProject(result);
                }
            }
        }
    }
}