using System;
using System.Collections.Generic;
using System.IO;
using NewPayDataTransformer.Engine;
namespace NewPayDataTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            x();
//            Core core = new Core();
//            core.Execute();
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
