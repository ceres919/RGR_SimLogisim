using Avalonia.Controls;
using Avalonia.Interactivity;
using SimLogisim.Models;
using SimLogisim.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using Avalonia.VisualTree;
using System.Net.Http.Headers;
using SimLogisim.Models.LogicalElements;
using Avalonia;
using Avalonia.Input;
using Avalonia.Controls.Shapes;
using System.Diagnostics;
using System.Reflection.Metadata;
using SimLogisim.Models.LoadAndSave;
using DynamicData;
using System.IO;
using System.Xml.Linq;

namespace SimLogisim.Views
{
    public partial class MainWindow : Window
    {
        private Point pointPointerPressed;
        private Point pointerPositionIntoShape;
        private IShape selectedElement;
        private ElementEntity? firstRectangle;
        private string firstEllipse;
        private int firstValue;
        protected bool isDragging, canChange;
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

        private void OnPointerPressed(object? sender, PointerPressedEventArgs pointerPressedEventArgs)
        {
            if (pointerPressedEventArgs.Source is Control control)
            {
                canChange = true;
                if (control.DataContext is ElementEntity rectangle)
                {
                     pointPointerPressed = pointerPressedEventArgs
                        .GetPosition(
                        this.GetVisualDescendants()
                        .OfType<Canvas>()
                        .FirstOrDefault(canvas => string.IsNullOrEmpty(canvas.Name) == false &&
                        canvas.Name.Equals("highLevelCanvas")));

                    if (pointerPressedEventArgs.Source is not Ellipse)
                    {
                        pointerPositionIntoShape = pointerPressedEventArgs.GetPosition(control);
                        isDragging = true;
                        this.PointerMoved += PointerMoveDragShape;
                        this.PointerReleased += PointerPressedReleasedDragShape;
                    }
                    else
                    {
                        if (this.DataContext is MainWindowViewModel viewModel && pointerPressedEventArgs.Source is Ellipse ellipse)
                        {
                            canChange = false;
                            if (ellipse.Name.Contains("Enter"))
                            {
                                firstEllipse = "Enter";
                                var index = Int32.Parse(ellipse.Name.Split("Enter")[1]);
                                firstValue = index;
                                viewModel.ElementsCollection.Add(new Connector
                                {
                                    StartPoint = pointPointerPressed,
                                    EndPoint = pointPointerPressed,
                                    EnterIndex = index,
                                    EnterRectangle = "first",
                                    Name = "Connector",
                                    FirstRectangle = rectangle,
                                });
                            }
                            else
                            {
                                firstEllipse = "Exit";
                                var index = Int32.Parse(ellipse.Name.Split("Exit")[1]);
                                firstValue = index;
                                viewModel.ElementsCollection.Add(new Connector
                                {
                                    StartPoint = pointPointerPressed,
                                    EndPoint = pointPointerPressed,
                                    Name = "Connector",
                                    ExitIndex = index,
                                    EnterRectangle = "second",
                                    FirstRectangle = rectangle,
                                });
                            }
                            firstRectangle = rectangle;

                            this.PointerMoved += PointerMoveDrawLine;
                            this.PointerReleased += PointerPressedReleasedDrawLine;
                        }
                    }
                }
            }
        }
        

        private void PointerMoveDragShape(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (pointerEventArgs.Source is Control control)
            {
                if (control.DataContext is ElementEntity rectangle && isDragging)
                {
                    Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());
                    
                    rectangle.StartPoint = new Point(
                        currentPointerPosition.X - pointerPositionIntoShape.X,
                        currentPointerPosition.Y - pointerPositionIntoShape.Y);
                    canChange = false;
                }
            }
        }

        private void PointerPressedReleasedDragShape(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            isDragging = false;
            this.PointerMoved -= PointerMoveDragShape;
            this.PointerReleased -= PointerPressedReleasedDragShape;
        }

        private void PointerMoveDrawLine(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                //Debug.WriteLine(sender);
                Connector connector = viewModel.ElementsCollection[viewModel.ElementsCollection.Count - 1] as Connector;
                Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                connector.EndPoint = new Point(
                        currentPointerPosition.X - 1,
                        currentPointerPosition.Y - 1);
            }
        } 

        private void PointerPressedReleasedDrawLine(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            this.PointerMoved -= PointerMoveDrawLine;
            this.PointerReleased -= PointerPressedReleasedDrawLine;

            var canvas = this.GetVisualDescendants()
                        .OfType<Canvas>()
                        .FirstOrDefault(canvas => string.IsNullOrEmpty(canvas.Name) == false &&
                        canvas.Name.Equals("highLevelCanvas"));

            var coords = pointerReleasedEventArgs.GetPosition(canvas);

            var element = canvas.InputHitTest(coords);
            MainWindowViewModel viewModel = this.DataContext as MainWindowViewModel;

            if (element is Ellipse ellipse && !ellipse.Name.Contains(firstEllipse))
            {
                if (ellipse.DataContext is ElementEntity rectangle && rectangle != firstRectangle)
                {
                    Connector connector = viewModel.ElementsCollection[viewModel.ElementsCollection.Count - 1] as Connector;
                    connector.SecondRectangle = rectangle;
                    if (firstEllipse == "Enter")
                    {
                        var index = Int32.Parse(ellipse.Name.Split("Exit")[1]);
                        connector.FirstRectangle.Enters[firstValue] = rectangle.Exits[index];
                        connector.ExitIndex = index;
                        connector.FirstRectangle.Logic();
                    }
                    else
                    {
                        var index = Int32.Parse(ellipse.Name.Split("Enter")[1]);
                        connector.SecondRectangle.Enters[index] = connector.FirstRectangle.Exits[firstValue];
                        connector.EnterIndex = index;
                        connector.SecondRectangle.Logic();
                    }
                    return;
                }
            }
            viewModel.ElementsCollection.RemoveAt(viewModel.ElementsCollection.Count - 1);
        }

        private void ChangeEnterValue(object sender, RoutedEventArgs routedEventArgs)
        {
            if (routedEventArgs.Source is Control control && canChange)
            {
                if (control.DataContext is ElementENTER enter)
                {
                    if (enter.Exits[0] == 0)
                    {
                        enter.Exits[0] = 1;
                        enter.ValueFill = "DarkBlue";
                    }
                    else
                    {
                        enter.Exits[0] = 0;
                        enter.ValueFill = "WhiteSmoke";

                    }
                    enter.Logic();
                    selectedElement = enter;
                    this.KeyUp += DeleteButton;
                }
            }
        }
        private void DeleteElement(object sender, RoutedEventArgs routedEventArgs)
        {
            if(routedEventArgs.Source is Control control)
            {
                if (control.DataContext is IShape element)
                {
                    selectedElement = element;
                    this.KeyUp+= DeleteButton;
                }
            }
        }
        private void DeleteButton(object sender, KeyEventArgs keyEventArgs)
        {
            if(keyEventArgs.Key == Key.Delete) 
            {
                if (this.DataContext is MainWindowViewModel viewModel)
                {
                    if(selectedElement is ElementEntity element)
                    {
                        viewModel.ElementsCollection.Remove(element);     
                    }
                    else
                    {
                        if(selectedElement is Connector connector)
                        {
                            viewModel.ElementsCollection.Remove(connector);
                        }
                    }
                }
            }
            this.KeyDown -= DeleteButton;
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
            XMLLoader loader = new XMLLoader();
            XMLSaver saver = new XMLSaver();
            JSONLoader json_loader = new JSONLoader();
            ProjectEntity open_project = json_loader.Load(result[0]);
            ObservableCollection<ProjectEntity> projects = new ObservableCollection<ProjectEntity>(loader.Load("../../../ProjectsStorage.xml"));
            open_project.DateOfVisit = DateTime.Now;
            MainWindow view;
            foreach ( var project in projects )
            {
                if (project.Name == project.Name && project.FileName == project.FileName)
                {
                    project.DateOfVisit = DateTime.Now;
                    var index = projects.IndexOf(project);
                    projects.Move(index, 0);
                    saver.Save(projects, "../../../ProjectsStorage.xml");
                    view = new MainWindow(open_project, "old");
                    view.Show();
                    this.Close();
                    return;
                }
            }
            projects.Insert(0, open_project);
            saver.Save(projects, "../../../ProjectsStorage.xml");
            view = new MainWindow(open_project, "old");
            view.Show();
            this.Close();
            return;
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
                    dataContext.SaveCurrentProject(result);
                }
            }
        }
    }
}