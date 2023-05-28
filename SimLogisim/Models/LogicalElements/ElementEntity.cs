using Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public abstract class ElementEntity : IShape
    {
        public abstract string Name { get; set; }
        public abstract Point StartPoint { get; set; }
        public abstract ObservableCollection<int> Enters { get; set; }
        public abstract ObservableCollection<int> Exits { get; set; }
        public abstract void Logic();
        public abstract event EventHandler<ChangeExitEventArgs> ChangeExit;
        public abstract event EventHandler<ChangeStartPointEventArgs> ChangeStartPoint;
    }
}
