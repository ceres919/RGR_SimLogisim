using Avalonia.Controls.Shapes;
using ReactiveUI;
using SimLogisim.Models;
using SimLogisim.Models.LoadAndSave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimLogisim.ViewModels
{
    public class LaunchWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ProjectEntity> projectsCollection;
        private ProjectEntity selectedProject;
        public LaunchWindowViewModel()
        {
            //ProjectsCollection = new ObservableCollection<ProjectEntity>();
            XMLLoader loader = new XMLLoader();
            projectsCollection = new ObservableCollection<ProjectEntity>(loader.Load("../../../ProjectsStorage.xml"));
            ProjectsCollection = new ObservableCollection<ProjectEntity>(projectsCollection.OrderBy(p => p.DateOfVisit));
        }

        public ProjectEntity LoadProject()
        {
            JSONLoader loader = new JSONLoader();
            XMLSaver saver = new XMLSaver();
            ProjectEntity project = loader.Load(SelectedProject.FileName);
            var index = ProjectsCollection.IndexOf(SelectedProject);
            SelectedProject.DateOfVisit = DateTime.Now;
            project.DateOfVisit = DateTime.Now;
            ProjectsCollection.Move(index, 0);
            saver.Save(ProjectsCollection, "../../../ProjectsStorage.xml");
            return project;
        }
        public ProjectEntity LoadProject(string path)
        {
            JSONLoader loader = new JSONLoader();
            XMLSaver saver = new XMLSaver();
            ProjectEntity project = loader.Load(path);
            foreach (var item in projectsCollection)
            {
                if(item.Name == project.Name && item.FileName == project.FileName)
                {
                    item.DateOfVisit = DateTime.Now;
                    var index = ProjectsCollection.IndexOf(item);
                    ProjectsCollection.Move(index, 0);
                    saver.Save(ProjectsCollection, "../../../ProjectsStorage.xml");
                    return item;
                }
            }
            project.DateOfVisit = DateTime.Now;
            ProjectsCollection.Insert(0, project);
            saver.Save(ProjectsCollection, "../../../ProjectsStorage.xml");
            return project;
        }
        public ProjectEntity SelectedProject
        {
            get => selectedProject;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedProject, value);
            }
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
