using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public class ChangeStartPointEventArgs : EventArgs
    {
        public Point OldStartPoint { get; set; }
        public Point NewStartPoint { get; set; }
    }
}
