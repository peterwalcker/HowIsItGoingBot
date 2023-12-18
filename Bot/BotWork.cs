using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using HowIsItGoingBot.Local;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace HowIsItGoingBot.Bot
{
    internal class BotWork
    {
        static bool SettingInterval = false; 
        internal static async void DoBotWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            try
            {
                var bot = new Telegram.Bot.TelegramBotClient(HowIsItGoingBot.Local.Settings.Token);
                //bot.SetWebhookAsync("");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                int offset = 0;
                while (true)
                {
                    Telegram.Bot.Types.Update[] updates = null;
                    try
                    {
                        updates = await bot.GetUpdatesAsync(offset);
                    }
                    catch (Exception ex)
                    {
                        Log.Save(ex.ToString());
                    }
                    foreach (var update in updates)
                    {
                        try
                        {
                            switch (update.Type)
                            {
                                case Telegram.Bot.Types.Enums.UpdateType.Message:
                                    var message = update.Message;
                                    if (message.Photo != null || message.Document != null)
                                    {
                                        Telegram.Bot.Types.File fileinfo = null;
                                        string picName = "RandomPicture_" + offset + ".jpg";
                                        if (message.Photo != null)
                                        {
                                            fileinfo = await bot.GetFileAsync(message.Photo.Last().FileId);
                                            if (message.Caption != null) picName = message.Caption + "_" + offset + ".jpg";
                                        }
                                        if (message.Document != null)
                                        {
                                            fileinfo = await bot.GetFileAsync(message.Document.FileId);
                                            picName = message.Document.FileName;
                                            if (message.Caption != null) picName = message.Caption + "_" + offset + ".jpg";
                                        }
                                        var filepath = fileinfo.FilePath;
                                        var downloadlink = "https://api.telegram.org/file/bot" + HowIsItGoingBot.Local.Settings.Token + "/" + filepath;
                                        if (fileinfo != null)
                                        {
                                            using (var client = new WebClient())
                                            {
                                                client.DownloadFile(downloadlink, Service.GetAppCatalog() + "\\pic\\inbox\\" + picName);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        switch (update.Message.Text.Split('@')[0])
                                        {
                                            case "/start":
                                                SetupChat(update.Message);
                                                SetInterval(bot, update.Message.Chat.Id);
                                                break;
                                            case "/interval":
                                                SetInterval(bot, update.Message.Chat.Id);
                                                break;
                                            case "/stop":
                                                StopForUser(update.Message.Chat.Id);
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    break;
                                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                                    HowIsItGoingBot.Local.User user = HowIsItGoingBot.Local.User.GetUser(update.CallbackQuery.Message.Chat.Id);
                                    switch (update.CallbackQuery.Data.Split('_')[0])
                                    {
                                        case "/best":
                                            //user.MarkAsDoneToday();
                                            SendApplauseMessage(bot, update.CallbackQuery.Message);
                                            break;
                                        case "/worst":
                                            //user.MarkAsDoneToday();
                                            SendSupportMessage(bot, update.CallbackQuery.Message);
                                            break;
                                        case "/t":
                                            user.SetRequestTime(update.CallbackQuery.Data.Split('_')[1], SettingInterval);
                                            long _chatId = update.CallbackQuery.Message.Chat.Id;
                                            await bot.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
                                            if (SettingInterval)
                                                AskForInterval(bot, _chatId, 1);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                        }
                        finally
                        {
                            offset = update.Id + 1;
                        }
                    }
                }
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static void SetupChat(Telegram.Bot.Types.Message Message)
        {
            HowIsItGoingBot.Local.User user = 
                new HowIsItGoingBot.Local.User(Message);
            user.Save();
        }
        
        internal static void SendApplauseMessage(Telegram.Bot.TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            //Message.RecieveMessage(bot, message, Caption.ApplauseCaption("RU"));
            Message.SendPicture(message.Chat.Id, bot, 1);
        }
        internal static void SendSupportMessage(Telegram.Bot.TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            //Message.RecieveMessage(bot, message, Caption.SupportCaption("RU"));
            Message.SendPicture(message.Chat.Id, bot, 2);
        }
        internal static void SendManual(Telegram.Bot.TelegramBotClient bot, long ChatID)
        {

        }

        internal static void StopForUser(long ChatID)
        {
            Local.User user = Local.User.GetUser(ChatID);
            user.IsActive = false;
            user.Save();
        }

        internal static void SetInterval(Telegram.Bot.TelegramBotClient bot, long ChatID)
        {
            SettingInterval = true;
            AskForInterval(bot, ChatID, 0);
        }

        internal static void AskForInterval(Telegram.Bot.TelegramBotClient bot, long ChatID, int step)
        {
            string Query = "";
            Localization localization = Localization.GetLoclization(ChatID);
            switch(step)
            {
                case 0:
                    Query = localization.Translation.BotStartQuery;
                    break;
                case 1:
                    Query = localization.Translation.BotStopQuery;
                    SettingInterval = false;
                    break;
            }
            Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup kb =
                HowIsItGoingBot.Bot.Keyboard.GetInlineTimeKeyboard();

            HowIsItGoingBot.Bot.Message.SendMessage(ChatID, Query, kb, bot);
        }
    }
}
