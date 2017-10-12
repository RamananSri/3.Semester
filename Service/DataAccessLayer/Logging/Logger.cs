using System;
using System.IO;

namespace DataAccessLayer.Logging
{
    public class Logger
    {
        private string path = ".\\log.txt";

        public void WriteToLog(int errorCode, string error, string message)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    logItem(sw, errorCode, error, message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    logItem(sw, errorCode, error, message);
                }
            }
        }

        private void logItem(StreamWriter sw,int errorCode, string error,string message)
        {
            sw.WriteLine();
            sw.WriteLine("Date: " + DateTime.Now);
            sw.WriteLine("    Error code: " + errorCode + " " + error);
            sw.WriteLine("    Message: " + message);
        }
    }
}