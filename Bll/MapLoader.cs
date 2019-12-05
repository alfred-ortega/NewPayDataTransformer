using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewPayDataTransformer.Model
{
    public class MapLoader
    {
        public Map FileMap {get;set;}

        public MapLoader()
        {
        }

        public Map LoadMap(string mappingFile)
        {
            string json = File.ReadAllText(mappingFile);

            return JsonSerializer.Deserialize<Map>(json);
//            return JsonConvert.DeserializeObject<Map>(json);
        }
    }//end class
}//end namespace


