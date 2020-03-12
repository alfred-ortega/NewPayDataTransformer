using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class EmployeeEftPaymentLoader
    {

        List<Employeeeft> employeeEfts;
        StringBuilder log = new StringBuilder();

        string newFileName = string.Empty;
        string[] rows;
        List<string> newRows;



        public EmployeeEftPaymentLoader(MockEmployeeDb db)
        {
            //PDW_EDS_EFT_PAYMT
            Logger.Log.Record("Instantiation of EmployeeEftPaymentLoader");
            setRows(Config.Settings.EftPaymentFile);
            Logger.Log.Record(rows.Length + " rows found");
            employeeEfts = new List<Employeeeft>();
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
            Logger.Log.Record("Begin EmployeeEftPaymentLoader.parseRows");

            NewPayContext context = new NewPayContext();
            int i = 0;
            foreach(string row in rows)
            {
                try
                {
                    string[] data = row.Split('~');
                    Employeeeft e = new Employeeeft();
                    e.AccountNumber = data[9];
                    e.RoutingNumber = data[21];
                    e.RecipientName = data[15];
                    e.PayPeriodEndDate = DateTime.Parse(data[32]);
                    string mockSSN = db.GetMockSSN(data[1]);
                    Employee emp = db.GetEmployeeBySSN(data[1], data[0]);
                    int empId = int.Parse(mockSSN.Substring(4));
                    e.EmployeeId = empId;
                    context.Add(e);
                    context.SaveChanges();
                    MockEmployeeEft me = new MockEmployeeEft(e.Id,e.PayPeriodEndDate,mockSSN);
                    createNewLine(data,me);
                }
                catch (System.Exception x)
                {
                    Logger.Log.Record(x.ToString());
//                    throw x;
                }
                i++;
                if(i % 100 == 0)
                    Logger.Log.Record(i.ToString() + " records parsed");

            }
            Logger.Log.Record("End EmployeeEftPaymentLoader.parseRows");            
        }

        void createNewLine(string[] data, MockEmployeeEft me)
        {
            data[1] = me.MockSsn;
            data[9] = me.AccountNumber;
            data[21] = me.RoutingNumber;
            data[15] = me.RecipientName;
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

            Logger.Log.Record("Begin EmployeeEftPaymentLoader.writeNewFile");            
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
            Logger.Log.Record("End EmployeeEftPaymentLoader.writeNewFile");

        }


    }//end class
}//end namespace