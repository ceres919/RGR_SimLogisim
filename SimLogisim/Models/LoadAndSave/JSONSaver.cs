using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimLogisim.Models.LoadAndSave
{
    public class JSONSaver
    {
        public void Save(ProjectEntity shapes, string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, shapes);
            }
        }
    }
}
