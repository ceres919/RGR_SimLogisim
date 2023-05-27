using Avalonia.Controls;
using Avalonia.Interactivity;
using SimLogisim.ViewModels;

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

        }
    }
}
