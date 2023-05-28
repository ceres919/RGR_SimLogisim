using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using SimLogisim.Views;

namespace TestSimSim
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();
            string ct = "IM";
            await Task.Delay(100);

            var button = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Content == "Создать новый проект");

            var box = mainWindow.GetVisualDescendants().OfType<TextBox>().First();

            button.RaiseEvent(new RoutedEventArgs());
            await Task.Delay(50);

            var neww = AvaloniaApp.GetMainWindow();

            await Task.Delay(50);

            Assert.Equal(neww.GetType(), typeof(MainWindow));
        }
    }
}