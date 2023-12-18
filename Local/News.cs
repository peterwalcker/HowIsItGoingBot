using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Local
{
    internal class News
    {
        internal static void SendNews(User user)
        {
            string newsPath = Service.GetAppCatalog() + "\\news\\" + DateTime.Now.ToShortDateString() + ".txt";
            string text = File.ReadAllText(newsPath);
            text = text.Replace("@username", user.FirstName);
            Bot.Message.SendMessage(user.ChatID, text);
        }
    }
}
