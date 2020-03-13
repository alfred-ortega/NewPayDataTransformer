using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;


namespace NewPayDataTransformer.Engine
{
    public class Core
    {
        List<Map> maps;
        MockEmployeeDb mockEmployeeDb;
        public void Execute()
        {
          
            if( Config.Settings.Agency.ToUpper() == "ALL")
            {
                string[] agencies = new string[] {"CB", "CU",  "NH", "OM", "RR","GS"};
                foreach(string agency in agencies)
                {
                    processAgency(agency);
                }
            }
            else
            {
                processAgency(Config.Settings.Agency);
            }
            backupDatabase();
        }

        private void backupDatabase()
        {
            Logger.Log.Record("Beginning Database Backup");
            string dbName = Config.Settings.ConnectionString.Replace("Data Source=",""); //"Data Source=D:\\Shared\\NEWPAY\\Database\\newpay.db
            string newDbName = string.Format(dbName.Substring(0,dbName.LastIndexOf(".")));
            newDbName += "_" + Config.Settings.PayPeriodEndDate + ".db";
            Console.WriteLine(newDbName);
            Logger.Log.Record(string.Format("{0} being backed up as {1}",dbName,newDbName));
            File.Copy(dbName,newDbName,true);
            Logger.Log.Record("Completed Database Backup");
        }

        private void processAgency(string agency)
        {
                Config.Settings.Agency = agency;
                Logger.Log.Record("Beginning process of Agency: " + Config.Settings.Agency);
                loadEmployees();
                loadUpdatedEmployees();
                loadMappingFiles();
                mockupFiles();
        }

        private void loadEmployees()
        {
            Logger.Log.Record("Begin Core.loadEmployees");
            string employeefile = Config.Settings.EmployeeFile;
            Logger.Log.Record("loading employees from " + Config.Settings.EmployeeFile);
            EmployeeLoader loader = new EmployeeLoader(employeefile);
            Logger.Log.Record("EmployeeLoader action complete");
            EmployeeValidator validator = new EmployeeValidator(loader);
            Logger.Log.Record("Employeevalidator action complete");
            validator.Validate();
            Logger.Log.Record("End Core.loadEmployees");
        }

        private void loadUpdatedEmployees()
        {
            
            NewPayContext context = new NewPayContext();
            Logger.Log.Record("Begin Core.loadUpdatedEmployees");
//            List<Employee> emps = context.Employee.Where(e => e.Agency == Config.Settings.Agency).ToList();
            List<Employee> emps = context.Employee.ToList();

            Logger.Log.Record(emps.Count.ToString() + " records loaded");
            mockEmployeeDb = new MockEmployeeDb( emps  );
            Logger.Log.Record("End Core.loadUpdatedEmployees");

        }


        private void loadMappingFiles()
        {
            maps = new List<Map>();
            string[] mappingFiles = Directory.GetFiles(Config.Settings.MappingDirectory);
            MapLoader ml = new MapLoader();
            foreach(var mappingFile in mappingFiles)
            {
                maps.Add(ml.LoadMap(mappingFile));
            }
        }

        private void mockupFiles()
        {
            try
            {
                string sourceFile = string.Empty;
                string destinationFile = string.Empty;
                StringBuilder sb = new StringBuilder();
                foreach(var map in maps)
                {
                    sourceFile = Config.Settings.FilesForMaskingDirectory + map.FileToMap;
                    destinationFile = Config.Settings.MaskedFilesDirectory + map.FileToMap;
                    Console.WriteLine(string.Format("{0} is being masked into {1}",sourceFile,destinationFile));
                    string[] rows = File.ReadAllLines(sourceFile);
                    foreach(var row in rows)
                    {
                        string[] data = row.Split('~');
                        string ssn = data[1];
                        MockEmployee me = mockEmployeeDb.GetMockEmployee(ssn);
                        foreach(Column c in map.Columns)
                        {
                            data[c.Position] = Swap(c,me);
                        }

                        string newRow = string.Empty;
                        for(int i = 0;i < data.Length; i++)
                        {
                            newRow += (data[i] + "~");
                        }
                        sb.AppendLine(newRow.Substring(0,newRow.Length-1));
                    }
                    File.WriteAllText(destinationFile,sb.ToString());
                    sb.Clear();
                }
            }
            catch (System.Exception x)
            {
                Logger.Log.Record(x.Message);
            }


        }

        private string Swap(Column column, MockEmployee me)
        {
            string retval = string.Empty;
            switch(column.MaskType)
            {
                case "SSN":
                    retval = me.Emplid;
                    break;
                case "FullName":
                    retval = me.FullName;
                    break;
                case "EmplId":
                    retval = me.Emplid;
                    break;  
                case "FirstName":
                    retval = me.FirstName;
                    break;  
                case "MiddleName":
                    retval = me.MiddleName;
                    break;  
                case "LastName":
                    retval = me.LastName;
                    break;  
                case "Suffix":
                    retval = me.Suffix;
                    break;  
                case "BeneSSN":
                    retval = me.BeneficiarySSN;
                    break;
                case "BeneFullName":
                    retval = me.BeneficiaryName;
                    break;
                case "BeneStreet":
                    retval = me.BeneficiaryStreetAddress;
                    break;
                case "BeneCity":
                    retval = me.BeneficiaryCity;
                    break;
                case "BeneState":
                    retval = me.BeneficiaryState;
                    break;
                case "BeneZip":
                    retval = me.BeneficiaryZipCode;
                    break;
                case "BeneZip2":
                    retval = me.BeneficiaryZipCode2;
                    break;
                case "HBIName":
                    retval = me.HBIPayeeName;
                    break;
                case "HBISSN":
                    retval = me.HBIPayeeSSN;
                    break;
                case "DateOfBirth":
                    retval = me.DateOfBirth.ToString("mmddyyyy"); //06301974
                    break;
                case "PreviousSSN":
                    retval = string.Empty;
                    break;
                case "StreetAddress":
                    retval = me.StreetAddress;
                    break;
                case "StreetAddress2":
                    retval = me.StreetAddress2;
                    break;
                case "City":
                    retval = me.City;
                    break;
                case "State":
                    retval = me.State;
                    break;
                case "ZipCode":
                    retval = me.ZipCode;
                    break;
                case "ZipCode2":
                    retval = me.ZipCode2;
                    break;
                default:
                    break;
            }
            return retval;

        }


        
    }//end class
}//end namespace