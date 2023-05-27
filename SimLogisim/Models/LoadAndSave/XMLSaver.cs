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
    public class XMLSaver
    {
        public void Save(ObservableCollection<ProjectEntity> data, string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<ProjectEntity>));

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                formatter.Serialize(stream, data);
            }
        }
    }
}
