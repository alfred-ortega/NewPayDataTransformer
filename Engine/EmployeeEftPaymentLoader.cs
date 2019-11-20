using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class EmployeeEftPaymentLoader
    {

        List<Employeeeft> eftRecords;
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
            eftRecords = new List<Employeeeft>();
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

        private void recordtime(DateTime start, DateTime end, string action)
        {
            TimeSpan ts = end.Subtract(start);
            log.AppendLine(string.Format("{0},{1}",action,ts.TotalMilliseconds));
        }

        void parseRows(MockEmployeeDb db)
        {
            Logger.Log.Record("Begin EmployeeEftPaymentLoader.parseRows");

            NewPayContext context = new NewPayContext();
            int i = 0;
            foreach(string row in rows)
            {
                DateTime start = DateTime.Now;
                string[] data = row.Split("~");
                DateTime end = DateTime.Now;
                recordtime(start,end,"Split Row");
                Employeeeft e = new Employeeeft();
                start = DateTime.Now;
                e.AccountNumber = data[9];
                e.RoutingNumber = data[21];
                e.RecipientName = data[15];
                e.PayPeriodEndDate = DateTime.Parse(data[32]);
                end = DateTime.Now;
                recordtime(start,end,"Parse data elements");
                start = DateTime.Now;
                string mockSSN = db.GetMockSSN(data[1]);
                end = DateTime.Now;
                recordtime(start,end,"Get Mock SSN");
//                Employee emp = db.GetEmployeeBySSN(data[1], data[0]);
                int empId = int.Parse(mockSSN.Substring(4));
                e.EmployeeId = empId;
                try
                {
                    start = DateTime.Now;
                    context.Add(e);
                    recordtime(start,DateTime.Now,"Add EmployeeEft to data context");
                    start = DateTime.Now;
                    context.SaveChanges();
                    recordtime(start,DateTime.Now,"Save changes to database");
                    MockEmployeeEft me = new MockEmployeeEft(i,e.PayPeriodEndDate,mockSSN);
                    start = DateTime.Now;
                    createNewLine(data,me);
                    recordtime(start,DateTime.Now,"Created new line in file");
                }
                catch (System.Exception x)
                {
                    throw;
                }
                i++;
                if(i % 100 == 0)
                    Logger.Log.Record(i.ToString() + " records parsed");

                if(i % 1000 == 0)
                {
                    File.WriteAllText(Config.Settings.LogDirectory + "ts.csv",log.ToString());
                    Console.WriteLine("done");
                }    

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
            Logger.Log.Record("Begin EmployeeEftPaymentLoader.writeNewFile");            
            using(StreamWriter streamWriter = new StreamWriter(newFileName,true))
            {
                try
                {
                    foreach(string row in newRows)
                    {
                        streamWriter.WriteLineAsync(row);
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