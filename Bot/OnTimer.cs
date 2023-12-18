using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HowIsItGoingBot.Local;

namespace HowIsItGoingBot.Bot
{
    internal static class OnTimer
    {
        internal static void OnTickEvent()
        {
            string ChatsPath = Service.GetAppCatalog() + "\\save\\";
            string[] chats = Directory.GetFiles(ChatsPath);

            foreach (string chat in chats)
            {
                User user = User.GetUser(Convert.ToInt64(Path.GetFileNameWithoutExtension(chat)));
                if (user.IsActive)
                {
                    if (user.CheckTimeToSendNews())
                    {
                        News.SendNews(user);
                    }
                    else if (user.CheckTimeToSendQuery())
                    {
                        Bot.Message.SendMessage(user.ChatID, Caption.GetCaption(Caption.CaptionType.HowIsItGoing, user.Locale) + ", " + user.FirstName + "?",
                            HowIsItGoingBot.Bot.Keyboard.GetInlineKeyboard(
                            new InlineButton(Caption.GetCaption(Caption.CaptionType.AllGood, user.Locale), "/best"),
                            new InlineButton(Caption.GetCaption(Caption.CaptionType.NotGood, user.Locale), "/worst")));
                        user.MarkAsDoneToday();
                    }
                    else if (user.CheckTimeToSendFunnyPicture())
                    {
                        HowIsItGoingBot.Bot.Message.SendPicture(user.ChatID);
                    }
                }
            }
        }
    }
}
