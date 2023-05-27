using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models
{
    public class ProjectEntity : AbstractNotifyPropertyChanged
    {
        private string name;
        private DateTime dateOfVisit;
        private string fileName;
        private ObservableCollection<CircuitEntity> circuits;
        public ProjectEntity() { }
        public string Name
        {
            get => name;
            set => SetAndRaise(ref name, value);
        }
        public DateTime DateOfVisit
        {
            get => dateOfVisit;
            set => SetAndRaise(ref dateOfVisit, value);
        }//= DateTime.Now;
        public string FileName
        {
            get => fileName;
            set => SetAndRaise(ref fileName, value);
        }

        public ObservableCollection<CircuitEntity> Circuits
        {
            get => circuits;
            set => SetAndRaise(ref circuits, value);
        }
        
    }
}
