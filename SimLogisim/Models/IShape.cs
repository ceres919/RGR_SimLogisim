using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models
{
    public abstract class IShape : AbstractNotifyPropertyChanged
    {
        string Name { get; set; }
        Point StartPoint { get; set; }
    }
}
