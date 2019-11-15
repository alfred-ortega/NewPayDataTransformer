using System;
using NewPayDataTransformer.Engine;
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
