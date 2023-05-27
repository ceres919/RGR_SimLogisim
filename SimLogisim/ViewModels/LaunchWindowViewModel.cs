using ReactiveUI;
using SimLogisim.Models;
using SimLogisim.Models.LoadAndSave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimLogisim.ViewModels
{
    public class LaunchWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ProjectEntity> projectsCollection;
        public LaunchWindowViewModel()
        {
            //ProjectsCollection = new ObservableCollection<ProjectEntity>();
            XMLLoader loader = new XMLLoader();
            ProjectsCollection = new ObservableCollection<ProjectEntity>(loader.Load("../../../ProjectsStorage.xml"));
            //ProjectsCollection.Add(
            //    new ProjectEntity()
            //    {
            //        Name = "New_Project",
            //        DateOfVisit = DateTime.Now,
            //        Circuits = new ObservableCollection<CircuitEntity> ()
            //        { 
            //            new CircuitEntity() { 
            //                Name = "Circ1",
            //                Elemets = new ObservableCollection<IShape> () {}
            //        }, },
            //    });
        }

        public ObservableCollection<ProjectEntity> ProjectsCollection
        {
            get => projectsCollection;
            set
            {
                this.RaiseAndSetIfChanged(ref projectsCollection, value);
            }
        }
    }
}
