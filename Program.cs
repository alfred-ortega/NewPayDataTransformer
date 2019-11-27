using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            //doIt();


            Core core = new Core();
            core.Execute();

//            reset();
        }

        static void doIt()
        {
            string[] fileNames = new string[] {"PDW_EDS_BASIC2", "PDW_EDS_BASIC3", "PDW_EDS_COP", "PDW_EDS_DEBT", "PDW_EDS_HBI", "PDW_EDS_HOME_LV", "PDW_EDS_LV", "PDW_EDS_LV_AGING", "PDW_EDS_MIL_LV", "PDW_EDS_MSC", "PDW_EDS_NTE_LIMIT", "PDW_EDS_OTH_EARN", "PDW_EDS_REST_LV", "PDW_EDS_RETIRE", "PDW_EDS_SHD_LV_DONOR", "PDW_EDS_SHD_LV_RECIP", "PDW_EDS_SLTAX", "PDW_EDS_TSP_PR_YTD", "PDW_EDS_WOP", "PDW_TPP_BASIC", "PDW_TPP_EARN_DED", "PDW_TPP_PAY_ADJ", "V_RETRO_TA", "V_RETRO_TAIND", "V_VALID_TA", "V_VALID_TAIND"};
            StringBuilder sb = new StringBuilder();
            foreach(string fileName in fileNames)
            {
                sb.AppendLine("{");
                sb.AppendLine("\t\"Action\" : \"Mask\",");
                sb.AppendLine("\t\"FileToMap\" : \"" + fileName  + "_{0}{1}.dat\", ");
                sb.AppendLine("\t\"Columns\" : [");
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\"MaskType\" : \"SSN\",");
                sb.AppendLine("\t\t\"Position\" : 1");
                sb.AppendLine("\t\t}");
                sb.AppendLine("\t]");
                sb.AppendLine("}");
                File.WriteAllText(@"D:\Shared\NewPay\Config\" + fileName + ".json",sb.ToString());
                sb.Clear();
            }
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
                    string select = "select count(*) from employee;";
                    command.CommandText = select;
                    int i = int.Parse(command.ExecuteScalar().ToString());
                    string[] tables = new string[] {"","","",""};
                    foreach(string table in tables)
                    {
                        string sql = "Drop table " + tables + ";";
                        command.CommandText = sql;
//                        command.ExecuteNonQuery();
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
