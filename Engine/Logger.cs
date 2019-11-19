using System;
using System.IO;
using System.Reflection;
namespace NewPayDataTransformer.Engine
{
    public sealed class Logger
    {
        private static readonly Logger log = new Logger();
        private static string logFileName = Config.Settings.LogDirectory + DateTime.Now.ToString("yyyy-MM-dd") + "_NewPayDataTransformerLog.log";
        public static Logger Log
        {
            get{return log;}
        }

        

        public void Record(string entry)
        {
            Record(LogType.Status,entry);
        }

        public void Record(LogType logType, string entry)
        {
            string ts = DateTime.Now.ToLongTimeString();
            string lType = string.Empty;
            if(logType == LogType.Error)
            {
                lType = "Error";
            }
            else
            {
                lType = "Status";
                System.Console.WriteLine(ts + " " + entry);
            }
            string message = string.Format("{0}\t{1}\t{2}",ts,lType,entry);
            using(StreamWriter streamWriter = new StreamWriter(logFileName,true))
            {
                try
                {
                    streamWriter.WriteLineAsync(message);
                    streamWriter.Close();
                }
                catch (System.Exception)
                {
                    //throw;
                }
            }

        }
        
    }//end class
}//end namespace