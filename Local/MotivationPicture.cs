using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Local
{
    internal static class MotivationPicture
    {
        const string _pictureFolder = "\\pic\\";
        internal enum PictureType { Applause, Support, Regular}
        internal static string GetPicture(PictureType type)
        {
            string _picType = type.ToString();
            
            string _path = Local.Service.GetAppCatalog() + _pictureFolder + _picType + "Picture\\";
            if (Directory.Exists(_path))
            {
                string[] _files = Directory.GetFiles(_path)./*Where(x => !x.Contains("_isBisy_")).*/ToArray();
                if (_files.Length > 0)
                {
                    /*
                    string _filename = _files[new Random().Next(_files.Length)];
                    string _newFilename = Path.GetDirectoryName(_filename) + "\\" + Path.GetFileNameWithoutExtension(_filename) + "_isBisy_" + Path.GetExtension(_filename);
                    File.Move(_filename, _newFilename);
                    return _newFilename;
                    */
                    return _files[new Random().Next(_files.Length)];
                }
            }
            return "";
        }
        /*
        internal static string GetAppalusePicture()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _pictureFolder + "ApplausePicture\\";
            if(Directory.Exists(_path))
            {
                string[] _files = Directory.GetFiles(_path);
                if(_files.Length > 0)
                {
                    string _filename = _files[new Random().Next(_files.Length)];
                    string _newFilename = Path.GetDirectoryName(_filename) + "\\" + Path.GetFileNameWithoutExtension(_filename) + "_isBisy_" + Path.GetExtension(_filename);
                    File.Move(_filename, _newFilename);
                    return _newFilename;
                }
            }
            return "";
        }

        internal static string GetSupportPicture()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string _path = info.DirectoryName + _pictureFolder + "SupportPicture\\";
            if (Directory.Exists(_path))
            {
                string[] _files = Directory.GetFiles(_path);
                if (_files.Length > 0)
                {
                    string _filename = _files[new Random().Next(_files.Length)];
                    string _newFilename = Path.GetDirectoryName(_filename) + "\\" + Path.GetFileNameWithoutExtension(_filename) + "_isBisy_" + Path.GetExtension(_filename);
                    File.Move(_filename, _newFilename);
                    return _newFilename;
                }
            }
            return "";
        }

        internal static string GetRegularPicture()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);
            
            string _path = info.DirectoryName + _pictureFolder + "RegularPicture\\";
            if (Directory.Exists(_path))
            {
                string[] _files = Directory.GetFiles(_path).Where(file => !file.Contains("_isBisy_")).ToArray();
                if (_files.Length > 0)
                {
                    string _filename = _files[new Random().Next(_files.Length)];
                    string _newFilename = Path.GetDirectoryName(_filename) + "\\" + Path.GetFileNameWithoutExtension(_filename) + "_isBisy_" + Path.GetExtension(_filename);
                    File.Move(_filename, _newFilename);
                    return _newFilename;
                }
            }
            return "";
        }
        */
    }
}
