using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
 //           test();
            Core core = new Core();
            core.Execute();
        }

        static void test()
        {
            string file = @"E:\Developers\AlO\NEWPAY\FilesForMasking\GS07DEC2019\test.csv";
            string[] rows = File.ReadAllLines(file);
            for (int i = 1; i < rows.Length; i++)
            {
                string row = rows[i];
                Console.WriteLine(row);
                string[] data = row.Split("\",\"");
                foreach (var item in data)
                {
                    string newItem = item.Replace("\"", string.Empty);
                    Console.WriteLine("\t" + newItem);
                }
            }

            Console.ReadLine();




        }








    }
}
