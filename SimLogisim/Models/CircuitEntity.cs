using Avalonia.Controls.Shapes;
using DynamicData.Binding;
using SimLogisim.Models.LogicalElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models
{
    public class CircuitEntity : AbstractNotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<IShape> elemets;
        public CircuitEntity() { }
        public string Name
        {
            get => name;
            set => SetAndRaise(ref name, value);
        }
        public ObservableCollection<IShape> Elemets
        {
            get => elemets;
            set => SetAndRaise(ref elemets, value);
        }
    }
}
