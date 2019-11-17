using System;
using Newtonsoft.Json;

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
            string json = System.IO.File.ReadAllText("appsettings.json");
            settings = JsonConvert.DeserializeObject<Config>(json);            
        }

        private Config()
        {
        }


        private string employeeFile;
        private string eftFile;
        private string nonEftFile;
        private string mappingDirectory;
        private string filesForMaskingDirectory;
        private string maskedFilesDirectory;
        private string filesForUnmaskingDirectory;
        private string unmaskedFilesDirectory;
        private string logDirectory;



        public string ConnectionString { get; set; }
        public string Agency { get; set; }
        public string Action { get; set; }
        public string PayPeriodEndDate { get; set; }
        public string EmployeeFile
        {
            get { return string.Format(employeeFile,Agency,PayPeriodEndDate); }
            set { employeeFile = value; }
        }

        public string EftFile
        {
            get { return eftFile; }
            set { eftFile = value; }
        }

        public string NonEftFile
        {
            get { return nonEftFile; }
            set { nonEftFile = value; }
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

        public string LogDirectory
        {
            get { return string.Format(logDirectory,Agency,PayPeriodEndDate); }
            set { logDirectory = value; }
        }
        
        


        

    }//end class
}//end namespace