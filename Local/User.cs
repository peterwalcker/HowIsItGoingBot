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
    /// <summary>
    /// Свойства пользователя, сериализуется в JSON
    /// </summary>
    public class User
    {
        public bool IsGroup { get; private set; }
        public bool IsActive { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public long ChatID { get; private set; }
        public bool NewsSent { get; private set; }
        public DateTime RequestTimeStart { get; set; }
        public DateTime RequestTimeEnd { get; set; }
        public string Locale { get; set; }

        /// <summary>
        /// Инициализация пользователя
        /// </summary>
        /// <param name="Message">Сообщение от пользователя или группы</param>
        internal User(Telegram.Bot.Types.Message Message)
        {
            IsGroup = Message.Chat.FirstName == null;
            IsActive = true;
            FirstName = IsGroup ? Message.Chat.Title : Message.Chat.FirstName;
            LastName = IsGroup ? "" : Message.Chat.LastName == null ? "" : Message.Chat.LastName;
            ChatID = Message.Chat.Id;
            NewsSent = false;
            RequestTimeStart = DateTime.MinValue;
            RequestTimeEnd = DateTime.MinValue;
            Locale = "RU";
        }
        public User (bool isGroup, string firstName, string lastName, long chatId, bool newsSent, 
            DateTime requestTimeStart, DateTime requestTimeEnd, string locale, bool isActive)
        {
            IsGroup = isGroup;
            IsActive = isActive;
            FirstName = firstName;
            LastName = lastName;
            ChatID = chatId;
            NewsSent = newsSent;
            RequestTimeStart = requestTimeStart;
            RequestTimeEnd = requestTimeEnd;
            Locale = locale;
        }

        /// <summary>
        /// Сохранить пользователя
        /// </summary>
        internal void Save()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            FileInfo info = new FileInfo(proc.MainModule.FileName);

            string SavePath = Path.GetFullPath(info.DirectoryName) + "\\save\\" + this.ChatID.ToString() + ".json";
            if (!Directory.Exists(Path.GetDirectoryName(SavePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
            if (!System.IO.File.Exists(SavePath))
                System.IO.File.Create(SavePath).Close();
            using (StreamWriter file = System.IO.File.CreateText(SavePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, this);
            }
        }

        /// <summary>
        /// Получить пользователя по ID чата
        /// </summary>
        /// <param name="ChatID">ID чата</param>
        /// <returns></returns>
        internal static User GetUser (long ChatID)
        {
            User user;
            string FilePath = Path.GetFullPath(Service.GetAppCatalog()) + "\\save\\" + ChatID.ToString() + ".json";
            using (StreamReader file = System.IO.File.OpenText(FilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                user = (User)serializer.Deserialize(file, typeof(User));
            }
            return user;
        }

        /// <summary>
        /// Устанавливает интервал, в который может прислать запрос
        /// </summary>
        /// <param name="Time">Время</param>
        /// <param name="Start">true - Установка начала интервала, false - Установка конца интервала</param>
        internal void SetRequestTime(string Time, bool Start)
        {
            DateTime _time = DateTime.MinValue;
            _time = _time.AddHours(Convert.ToDouble(Time));
            _time = DateTime.Now.Date + _time.TimeOfDay;
            switch (Start)
            {
                case true:
                    this.RequestTimeStart = _time;
                    break;
                case false:
                    this.RequestTimeEnd = _time;
                    if (this.RequestTimeEnd < DateTime.Now)
                        MarkAsDoneToday();
                    break;
            }
            this.Save();
        }
        /// <summary>
        /// Устанавливает дату срабатывания на завтра.
        /// </summary>
        internal void MarkAsDoneToday()
        {
            RequestTimeStart = DateTime.Now.Date.AddDays(1) + RequestTimeStart.TimeOfDay;
            RequestTimeEnd = DateTime.Now.Date.AddDays(1) + RequestTimeEnd.TimeOfDay;
            NewsSent = false;
            Save();
        }

        internal bool CheckTimeToSendNews()
        {
            if (DateTime.Now.Date >= this.RequestTimeStart.Date &&
                DateTime.Now.TimeOfDay > this.RequestTimeStart.TimeOfDay &&
                DateTime.Now.TimeOfDay < this.RequestTimeEnd.TimeOfDay)
            {
                if (this.NewsSent == false)
                {
                    this.NewsSent = true;
                    Save();
                    return true;
                }
                return false;
            }
            return false;
        }

        internal bool CheckTimeToSendQuery()
        {
            if(DateTime.Now.Date >= this.RequestTimeStart.Date &&
                DateTime.Now.TimeOfDay > this.RequestTimeStart.TimeOfDay &&
                DateTime.Now.TimeOfDay < this.RequestTimeEnd.TimeOfDay)
            {
                double _remainingInterval = (RequestTimeEnd.TimeOfDay - DateTime.Now.TimeOfDay).TotalSeconds;
                double _fullInterval = (RequestTimeEnd.TimeOfDay - RequestTimeStart.TimeOfDay).TotalSeconds;
                double _threshold = new Random().NextDouble();
                
                return 1 - (_remainingInterval / _fullInterval) > _threshold;
            }
            return false;
        }

        internal bool CheckTimeToSendFunnyPicture()
        {
            if (DateTime.Now.TimeOfDay > this.RequestTimeStart.TimeOfDay && DateTime.Now.TimeOfDay < this.RequestTimeEnd.TimeOfDay)
            {
                DateTime _currTime = RequestTimeEnd.Date + DateTime.Now.TimeOfDay;
                double _remainingInterval = (RequestTimeEnd - _currTime).TotalSeconds;
                double _fullInterval = (RequestTimeEnd - RequestTimeStart).TotalSeconds;
                double _threshold = new Random().NextDouble();
                return 1 - (_remainingInterval / _fullInterval) > _threshold;
            }
            return false;
        }
    }
}
