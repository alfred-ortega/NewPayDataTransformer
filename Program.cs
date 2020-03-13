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
            try
            {
                NewPayContext context = new NewPayContext();
                var items = context.Employee;
                foreach (var item in items)
                {
                    Console.WriteLine(item.FullName);
                }
            }
            catch (Exception x)
            {
                throw x;
            }


            //Core core = new Core();
            //core.Execute();
        }

    








    }
}
