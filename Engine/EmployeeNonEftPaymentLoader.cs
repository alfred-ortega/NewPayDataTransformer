using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class EmployeeNonEftPaymentLoader
    {
        List<Employeenoneft> employeenonefts;
        StringBuilder log = new StringBuilder();
        string newFileName = string.Empty;
        string[] rows;
        List<string> newRows;

        public EmployeeNonEftPaymentLoader(MockEmployeeDb db)
        {
            Logger.Log.Record("Instantiation of EmployeeNonEftPaymentLoader");
            setRows(Config.Settings.NonEftFile);
            Logger.Log.Record(rows.Length + " rows found");
            employeenonefts = new List<Employeenoneft>();
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
            Logger.Log.Record("Begin EmployeeNonEftPaymentLoader.parseRows");
            NewPayContext context = new NewPayContext();
            int i = 0;
            foreach(string row in rows)
            {
                string[] data = row.Split("~");
                Employeenoneft e = new Employeenoneft();
                e.RecipientName = data[10];
                e.StreetAddress = data[11];
                e.StreetAddress2 = data[12];
                e.City = data[13];
                e.State = data[14];
                e.ZipCode = data[15];
                e.ZipCode2 = data[16];
                e.HomePhone = data[17];
                string mockSSN = db.GetMockSSN(data[1]);
                Employee emp = db.GetEmployeeBySSN(data[1], data[0]);
                int empId = int.Parse(mockSSN.Substring(4));
                e.EmployeeId = empId;
                try
                {
                    context.Add(e);
                    context.SaveChanges();
                    MockEmployeeNonEft me = new MockEmployeeNonEft(e.Id,e.PayPeriodEndDate,mockSSN);
                    createNewLine(data,me);
                }
                catch (System.Exception)
                {
                    
                    throw;
                }

            }
        }

        void createNewLine(string[] data, MockEmployeeNonEft me)
        {
            data[1] = me.MockSsn;
            data[10] = me.RecipientName;            
            data[11] = me.StreetAddress;
            data[12] = me.StreetAddress2;
            data[13] = me.City;
            data[14] = me.State;
            data[15] = me.ZipCode;
            data[16] = me.ZipCode2;
            data[17] = me.HomePhone;

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

            Logger.Log.Record("Begin EmployeeNonEftPaymentLoader.writeNewFile");            
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
            Logger.Log.Record("End EmployeeNonEftPaymentLoader.writeNewFile");
        }


    }//end class
}//end namespace