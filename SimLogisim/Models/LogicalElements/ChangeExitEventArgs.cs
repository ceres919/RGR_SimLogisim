using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LogicalElements
{
    public class ChangeExitEventArgs : EventArgs
    {
        public ObservableCollection<int> Exits { get; set; }
    }
}
