using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimLogisim.Models.LoadAndSave
{
    public class XMLLoader
    {
        public IEnumerable<ProjectEntity> Load(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<ProjectEntity>));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ObservableCollection<ProjectEntity>? newList = formatter.Deserialize(fs) as ObservableCollection<ProjectEntity>;
                if (newList == null)
                {
                    newList = new ObservableCollection<ProjectEntity>();
                }
                return newList;
            }
        }
    }
}
