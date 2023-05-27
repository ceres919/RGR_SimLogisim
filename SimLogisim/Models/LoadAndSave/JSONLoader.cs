using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LoadAndSave
{
    public class JSONLoader
    {
        public ProjectEntity Load(string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            using (StreamReader file = File.OpenText(path))
            {
                ProjectEntity? shapes = (ProjectEntity)serializer.Deserialize(file, typeof(ProjectEntity));
                return shapes;
            }
        }
    }
}
