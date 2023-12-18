using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Local
{
    internal class Service
    {
        internal static string GetAppCatalog()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            return info.DirectoryName;
        }
    }
}
