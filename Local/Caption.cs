using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HowIsItGoingBot.Local
{
    internal static class Caption
    {
        const string _captionFolder = "\\captions\\";
        internal enum CaptionType { HowIsItGoing, AllGood, NotGood, Applause, Support, RegularMessage}
        internal static string GetCaption(CaptionType type, string Lang)
        {
            string _captionType = type.ToString();
            string _path = Service.GetAppCatalog() + _captionFolder + _captionType +"Caption_" + Lang + ".txt";
            string Default = "";
            switch (type)
            {
                case CaptionType.HowIsItGoing:
                    Default = "How is it going?";
                    break;
                case CaptionType.AllGood:
                    Default = "Nice!";
                    break;
                case CaptionType.NotGood:
                    Default = "That must be hard.";
                    break;
                case CaptionType.Applause:
                    Default = "Great!";
                    break;
                case CaptionType.Support:
                    Default = "You're doing your best.";
                    break;
                case CaptionType.RegularMessage:
                    Default = "Look what i've found!";
                    break;
            }    
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }
        /*
        internal static string HowIsItGoingCaption(string Lang, string Default = "How is it going?")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "HowIsItGoingCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }

        internal static string AllGoodCaption(string Lang, string Default = "Nice!")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "AllGoodCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }

        internal static string NotGoodCaption(string Lang, string Default = "That must be hard.")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "NotGoodCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }

        internal static string ApplauseCaption(string Lang, string Default = "Great!")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "ApplauseCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }

        internal static string SupportCaption(string Lang, string Default = "You're doing your best.")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "SupportCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }

        internal static string RegularMessageCaption(string Lang, string Default = "Look what i've found!")
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _captionFolder + "RegularMessageCaption_" + Lang + ".txt";
            if (File.Exists(_path))
            {
                string[] _captions = File.ReadAllLines(_path);
                return _captions[new Random().Next(0, _captions.Length)];
            }
            return Default;
        }
        */
    }
}
