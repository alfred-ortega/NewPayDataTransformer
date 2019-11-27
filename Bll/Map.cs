using System;
using System.Collections.Generic;
using NewPayDataTransformer.Engine;
namespace NewPayDataTransformer.Model
{
    public class Map
    {
        private string fileToMap;

        public string Action { get; set; }
        public string FileToMap { 
            get {return string.Format(fileToMap,Config.Settings.Agency,Config.Settings.PayPeriodEndDate);} 
            set {fileToMap = value;} 
        }
        public List<Column> Columns {get; set;}

        public Map()
        {

        }

    }//end class
}//ebd namespace