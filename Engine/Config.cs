using System;
using System.Text.Json;
using System.Text.Json.Serialization;

//using Newtonsoft.Json;

namespace NewPayDataTransformer.Engine
{
    public class Config
    {

        private static readonly Config settings = new Config();

        public static Config Settings
        {
            get{return settings;}
            
        }

        static Config()
        {
            string json = System.IO.File.ReadAllText(@"E:\Developers\AlO\source\repos\NewPayDataTransformer\appsettings.json");
            settings = JsonSerializer.Deserialize<Config>(json);
        }

        private Config()
        {
        }


        private string employeeFile;
        private string employeeIdNumberFile;

        private string mappingDirectory;
        private string filesForMaskingDirectory;
        private string maskedFilesDirectory;
        private string filesForUnmaskingDirectory;
        private string unmaskedFilesDirectory;

        public string ConnectionString { get; set; }
        public string Agency { get; set; }
        public string Action { get; set; }
        public string PayPeriodEndDate { get; set; }
        public string EmployeeFile
        {
            get { return string.Format(employeeFile,Agency,PayPeriodEndDate); }
            set { employeeFile = value; }
        }

        public string EmployeeIdNumberFile
        {
            get { return string.Format(employeeIdNumberFile,Agency,PayPeriodEndDate); }
            set { employeeIdNumberFile = value; }
        }

        
        
        
        public string MappingDirectory
        {
            get { return string.Format(mappingDirectory,Agency,PayPeriodEndDate); }
            set { mappingDirectory = value; }
        }
        
        public string FilesForMaskingDirectory
        {
            get { return string.Format(filesForMaskingDirectory,Agency,PayPeriodEndDate); }
            set { filesForMaskingDirectory = value; }
        }

        public string MaskedFilesDirectory
        {
            get { return string.Format(maskedFilesDirectory,Agency,PayPeriodEndDate); }
            set { maskedFilesDirectory = value; }
        }
        

        public string FilesForUnmaskingDirectory
        {
            get { return string.Format(filesForUnmaskingDirectory,Agency,PayPeriodEndDate); }
            set { filesForUnmaskingDirectory = value; }
        }
        
        public string UnmaskedFilesDirectory
        {
            get { return string.Format(unmaskedFilesDirectory,Agency,PayPeriodEndDate); }
            set { unmaskedFilesDirectory = value; }
        }

        public string LogDirectory {get ; set;}
        
        


        

    }//end class
}//end namespace