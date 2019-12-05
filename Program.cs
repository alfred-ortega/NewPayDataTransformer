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
            Core core = new Core();
            core.Execute();
        }

    








    }
}
