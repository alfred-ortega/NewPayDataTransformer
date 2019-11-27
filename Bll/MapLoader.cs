using System;
using System.IO;
using Newtonsoft.Json;

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
            return JsonConvert.DeserializeObject<Map>(json);
        }
    }//end class
}//end namespace


