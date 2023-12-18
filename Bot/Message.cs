using HowIsItGoingBot.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace HowIsItGoingBot.Bot
{
    internal class Message
    {
        /// <summary>
        /// Отправляет пользователю ответ
        /// </summary>
        /// <param name="bot">Активный бот</param>
        /// <param name="Message">Сообщение для ответа</param>
        /// <param name="RecieveText">Текст ответа</param>
        /// <param name="InlineKeyboard">Inline-кнопки если нужны</param>
        internal static async void RecieveMessage(
            Telegram.Bot.TelegramBotClient bot, 
            Telegram.Bot.Types.Message Message,
            string RecieveText,
            Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup InlineKeyboard = null)
        {
            await bot.SendTextMessageAsync(
                Message.Chat.Id, 
                RecieveText, 
                null, null, null, null, null, 
                Message.MessageId, 
                null, 
                InlineKeyboard);
        }

        /// <summary>
        /// Отправляет картинку с подписью
        /// </summary>
        /// <param name="ChatID">Чат, в который отправляем</param>
        /// <param name="bot">Текущий бот</param>
        /// <param name="type">Тип картинки</param>
        internal static async void SendPicture(
            long ChatID,
            Telegram.Bot.TelegramBotClient bot = null,
            int type = 0)
        {
            string _picturePath = "";
            string _message = "";
            Local.User user = User.GetUser(ChatID);
            switch(type)
            {
                case 0:
                    _picturePath = MotivationPicture.GetPicture(MotivationPicture.PictureType.Regular);
                    _message = Caption.GetCaption(Caption.CaptionType.RegularMessage, user.Locale);
                    break;
                case 1:
                    _picturePath = MotivationPicture.GetPicture(MotivationPicture.PictureType.Applause);
                    _message = Caption.GetCaption(Caption.CaptionType.Applause, user.Locale);
                    break;
                case 2:
                    _picturePath = MotivationPicture.GetPicture(MotivationPicture.PictureType.Support);
                    _message = Caption.GetCaption(Caption.CaptionType.Support, user.Locale);
                    break;
            }

            if (bot == null)
                bot = new Telegram.Bot.TelegramBotClient(Settings.Token);

            try
            {
                using (var stream = File.Open(_picturePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await bot.SendPhotoAsync(ChatID, stream, _message);
                }
            }
            catch (Exception ex)
            {
                Log.Save(ex.ToString());
            }
            finally
            {
                //File.Move(_picturePath, _picturePath.Replace("_isBisy_", ""));
            }
        }
        /// <summary>
        /// Самостоятельно отправляет сообщение пользователю.
        /// </summary>
        /// <param name="ChatID">UserSettings.ChatID</param>
        /// <param name="Message">Текст сообщения</param>
        /// <param name="bot">Запущенный бот, если есть</param>
        internal static async void SendMessage(
            long ChatID, 
            string Message, 
            Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup InlineKeyboard = null, 
            Telegram.Bot.TelegramBotClient bot = null)
        {
            if (bot == null)
            {
                bot = new Telegram.Bot.TelegramBotClient(HowIsItGoingBot.Local.Settings.Token);
            }
            await bot.SendTextMessageAsync(
                ChatID, 
                Message, 
                null, null, null, null, null, null, null,
                InlineKeyboard);
        }
    }
}
