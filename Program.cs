using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            //y();
            Core core = new Core();
            core.Execute();

//            reset();
        }


        static void reset()
        {
            resetFiles();


        }
        static void resetFiles()
        {
            string path = @"D:\Shared\NewPay\MaskedFiles\\";
            Directory.Delete(path,true);
        }

        static void resetDatabase()
        {
            dropTables();
        }

        static void dropTables()
        {
            using (MySqlConnection connection = new MySqlConnection(Config.Settings.ConnectionString))
            {
                using(MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    connection.Open();
                    string[] tables = new string[] {"","","",""};
                    foreach(string table in tables)
                    {
                        string sql = "Drop table " + tables + ";";
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }//end command
                
            }//end connetion
        }

 
        static void y()
        {
            using(NewPayContext context = new NewPayContext())
            {
                Employee emp = new Employee();
                emp.FirstName = "Alfred";
                emp.LastName = "Ortega";
                emp.Suffix = "III";
                emp.StreetAddress = "2821 NE Marywood Lane";
                emp.City = "Lees Summit";
                emp.State = "MO";
                emp.ZipCode = "64086";
                emp.Ssn = "574942760";
                emp.PayPeriodEndDate = new DateTime(2019,11,9);
                emp.Agency="GS";
                emp.DateOfBirth = new DateTime(1974,6,30);
                context.Employee.Add(emp);
                context.SaveChanges();
            } 

        }
    

        static void x()
        {
            string[] files = Directory.GetFiles(Config.Settings.FilesForMaskingDirectory);
            foreach(string file in files)
            {
                doSSNCount(file);
            }

        }


        static void doSSNCount(string file)
        {
            try
            {
                List<string> socials = new List<string>();
                string[] lines = File.ReadAllLines(file);
                foreach(string line in lines)
                {
                    string[] data = line.Split('~');
                    socials.Add(data[1]);
                }
                socials.Sort();
                string currSSN = string.Empty;
                string prevSSN = string.Empty;
                foreach(string social in socials)
                {
                    currSSN = social;
                    if(currSSN == prevSSN)
                    {
                        Console.WriteLine(file);
                        break;
                    }
                    prevSSN = social;
                }
                
                
            }
            catch (System.Exception x)
            {
//                Console.WriteLine("Couldn't parse file " + file);                
//                Console.WriteLine(x.ToString());
            }
        }





    }
}
