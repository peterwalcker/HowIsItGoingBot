using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HowIsItGoingBot.Local
{
    public class Localization
    {
        const string _defaultLocalization = "RU";
        public string Lang { get; set; }
        public Translation Translation { get; set; }

        public static Localization GetLoclization(long ChatId)
        {
            Localization localization;
            User user = User.GetUser(ChatId);
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string FilePath = Path.GetFullPath(info.DirectoryName) + "\\localization\\localization_" + user.Locale + ".json";
            if(!System.IO.File.Exists(FilePath)) 
                FilePath = Path.GetFullPath(info.DirectoryName) + "\\localization\\localization_" + _defaultLocalization + ".json";
            using (StreamReader file = System.IO.File.OpenText(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                localization = (Localization)serializer.Deserialize(file, typeof(Localization));
            }
            return localization;
        }
    }

    public class Translation
    {
        public string BotStartQuery { get; set; }
        public string BotStopQuery { get; set; }
    }
}
