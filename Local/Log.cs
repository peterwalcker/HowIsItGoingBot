using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Local
{
    internal static class Log
    {
        const string _logFileName = "log.txt";
        internal static void Save(string Text)
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName); 
            
            string _logFile = info.DirectoryName + "\\" + DateTime.Now.Date.ToString() + "_" + _logFileName;
            if (!File.Exists(_logFile))
                File.Create(_logFile).Close();
            using (StreamWriter sw = File.AppendText(_logFile))
            {
                sw.WriteLine(DateTime.Now.TimeOfDay.ToString() + " - " + Text);
            }
        }
    }
}
