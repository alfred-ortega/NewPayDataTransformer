using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class EmployeeEftAddressLoader
    {
        List<Employeeeftaddress> employeeeftaddresses;
        StringBuilder log = new StringBuilder();
        string newFileName = string.Empty;
        string[] rows;
        List<string> newRows;

        public EmployeeEftAddressLoader(MockEmployeeDb db)
        {
            //PDW_EDS_EFT_ADDR
            Logger.Log.Record("Instantiation of EmployeeEftAddressLoader");
            setRows(Config.Settings.EftAddressFile);
            Logger.Log.Record(rows.Length + " rows found");
            employeeeftaddresses = new List<Employeeeftaddress>();
            newRows = new List<string>();
            parseRows(db);
            writeNewFile();

        }

        void setRows(string eftFile)
        {
            if(!File.Exists(eftFile))
                throw new FileNotFoundException(string.Format("The File '{0}' was not found.",eftFile));

            rows = File.ReadAllLines(eftFile);
            newFileName = eftFile.Replace(Config.Settings.FilesForMaskingDirectory,Config.Settings.MaskedFilesDirectory);
        }//end setRows

        void parseRows(MockEmployeeDb db)
        {
            Logger.Log.Record("Begin EmployeeEftAddressLoader.parseRows");

            NewPayContext context = new NewPayContext();
            int i = 0;
            foreach(string row in rows)
            {
                string[] data = row.Split("~");
                Employeeeftaddress e = new Employeeeftaddress();
                e.AccountNumber = data[9];
                e.BankName = data[10];
                e.BankStreetAddress = data[11];
                e.BankStreetAddress2 = data[12];
                e.PayPeriodEndDate = DateTime.Parse(data[25]);
                string mockSSN = db.GetMockSSN(data[1]);
                MockEmployee mockEmployee = db.GetMockEmployee(data[1], data[0]);
                Employee emp = db.GetEmployeeBySSN(data[1], data[0]);
                int empId = int.Parse(mockSSN.Substring(4));
                e.EmployeeId = empId;
                try
                {
                    context.Add(e);
                    context.SaveChanges();
                    MockEmployeeEftAddress me = new MockEmployeeEftAddress(e.Id,e.PayPeriodEndDate,mockEmployee);
                    createNewLine(data,me);


                }
                catch (System.Exception)
                {
                    
                    throw;
                }

            }

        }





        void createNewLine(string[] data, MockEmployeeEftAddress me)
        {
            data[1] = me.MockSsn;
            data[9] = me.AccountNumber;            
            data[10] = me.BankName;
            data[11] = me.BankStreetAddress;
            data[12] = me.BankStreetAddress2;
            data[13] = me.Employee.FullName;
            data[14] = me.Employee.StreetAddress;
            data[15] = me.Employee.StreetAddress2;
            data[16] = me.Employee.City + ", " + me.Employee.State + " " + me.Employee.FullZipCode;

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i] + "~" );
            }
            string row = sb.ToString().Substring(0,sb.ToString().Length-1);
            newRows.Add(row);
        }


        void writeNewFile()
        {
            //D:\Shared\NEWPAY\MaskedFiles\RR09NOV2019\PDW_EDS_EFT_PAYMT_RR09NOV2019.dat

            Logger.Log.Record("Begin EmployeeEftAddressLoader.writeNewFile");            
            string directory = newFileName.Substring(0,newFileName.LastIndexOf("\\"));
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            Logger.Log.Record(string.Format("Preparing to write {0} rows into {1}",newRows.Count,newFileName));
            using(StreamWriter streamWriter = new StreamWriter(newFileName,true))
            {
                try
                {
                    foreach(string row in newRows)
                    {
                        streamWriter.WriteLine(row);
                    }
                    streamWriter.Close();
                }
                catch (System.Exception)
                {
                    //throw;
                }
            }
            Logger.Log.Record("End EmployeeEftAddressLoader.writeNewFile");
        }



    }//end class
}//end namespace