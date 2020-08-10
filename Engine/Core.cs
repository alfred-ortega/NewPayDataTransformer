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
//            backupDatabase();
        }

        private void backupDatabase()
        {
            Logger.Log.Record("Beginning Database Backup");
            string dbName = Config.Settings.ConnectionString.Replace("Data Source=",""); //"Data Source=D:\\Shared\\NEWPAY\\Database\\newpay.data
            string newDbName = string.Format(dbName.Substring(0,dbName.LastIndexOf(".")));
            newDbName += "_" + Config.Settings.PayPeriodEndDate + ".data";
            Console.WriteLine(newDbName);
            Logger.Log.Record(string.Format("{0} being backed up as {1}",dbName,newDbName));
            File.Copy(dbName,newDbName,true);
            Logger.Log.Record("Completed Database Backup");
        }

        private void processAgency(string agency)
        {
                Config.Settings.Agency = agency;
                Logger.Log.Record("Beginning process of Agency: " + Config.Settings.Agency);
//                loadEmployees();
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
            string[] mappingFiles = Directory.GetFiles(Config.Settings.MappingDirectory,"*.json");
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
                StringBuilder commaText = new StringBuilder();
                foreach(var map in maps)
                {
                    bool isLastColumnMasked = false;
                    bool isFirstColumnMasked = false;
                    sourceFile = Config.Settings.FilesForMaskingDirectory + map.FileToMap;
                    destinationFile = Config.Settings.MaskedFilesDirectory + map.FileToMap;
                    Console.WriteLine(string.Format("{0} is being masked into {1}",sourceFile,destinationFile));
                    string[] rows = File.ReadAllLines(sourceFile);

                    string header = rows[0];
                    string commaHeader = rows[0].Replace("|", ",");
                    sb.AppendLine(header);
                    commaText.AppendLine(commaHeader);
                    int rowCount = rows.Length;
                    Console.WriteLine(string.Format("\t{0} rows in file ", rowCount));
                    int lastColumnNumber = 0;
                    for (int i = 1; i < rowCount; i++)
                    {
                        string[] data = rows[i].Split("\"|\"");  //string[] data = rows[i].Split("\",\""); Change comma to pipe
                        lastColumnNumber = data.Length - 1;
                        string emplId = data[map.EmployeeIdColumn].Replace("\"",string.Empty);

                        MockEmployee me = mockEmployeeDb.GetMockEmployee(emplId);
                        foreach (Column c in map.Columns)
                        {
                            if (c.Position == lastColumnNumber)
                            {
                                isLastColumnMasked = true;
                            }

                            if (c.Position == 0)
                            {
                                isFirstColumnMasked = true;
                            }

                            data[c.Position] = Swap(c, me);
                        }

                        string newRow = string.Empty;
                        if (map.EmployeeIdColumn == 0 || isFirstColumnMasked)
                            newRow = "\"";

                        for (int j = 0; j < data.Length; j++)
                        {
                            newRow += (data[j] + "\"|\""); // newRow += (data[j] + "\",\""); Change comma to pipe
                        }

                        if (isLastColumnMasked)
                        {
                            string line = newRow.Substring(0, newRow.Length - 2);
                            sb.AppendLine(line);
                            commaText.AppendLine(line.Replace("|", ","));
                        }
                        else
                        {
                            string line = newRow.Substring(0, newRow.Length - 3);
                            sb.AppendLine(line);
                            commaText.AppendLine(line.Replace("|", ","));
                        }

                        if (i % 100 == 0)
                            Console.WriteLine(string.Format("{0} of {1} Processed ", i, rowCount));

                    }
                    File.WriteAllText(destinationFile,sb.ToString());
                    File.WriteAllText(destinationFile + ".txt", commaText.ToString());
                    sb.Clear();
                    commaText.Clear();
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
                    retval = me.Ssn;
                    break;
                case "FullName":
                    retval = me.FullName;
                    break;
                case "EMPLID":
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
                case "DateOfBirth":
                    retval = me.DateOfBirth; //06301974
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
                    retval = string.Empty;
                    break;
                case "RoutingNumber":
                    retval = "054001220";
                    break;
                case "BankName":
                    retval = "Baker Street Bank and Trust";
                    break;
                case "County":
                    //retval = me.County;
                    break;
                case "LegacyEmploymentNumber":
                    retval = me.LegacyEmploymentNumber;
                    break;
                case "EMPTY":
                    retval = string.Empty;
                    break;
                default:
                    break;
            }
            return retval;

        }


        
    }//end class
}//end namespace