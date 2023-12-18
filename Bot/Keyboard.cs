using HowIsItGoingBot.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Bot
{
    static internal class Keyboard
    {
        /// <summary>
        /// Возвращает набор inline-кнопок в 3 столбца максимум
        /// </summary>
        /// <param name="Buttons">Набор inline-кнопок</param>
        /// <returns></returns>
        static internal Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup GetInlineKeyboard(params InlineButton[] Buttons)
        {
            int columns = Buttons.Length / 3 > 0 ? 3 : Buttons.Length;
            int lines = Buttons.Length / 3;
            Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton[][] buttons = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton[lines + 1][];
            for (int i = 0; i <= lines; i++)
            {
                buttons[i] = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton[columns];
                for (int j = 0; j < columns; j++)
                {
                    if (i * 3 + j < Buttons.Length)
                    {
                        buttons[i][j] = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton(Buttons[i * 3 + j].Name)
                        { CallbackData = Buttons[i * 3 + j].CallbackData.ToString() };
                    }
                    else
                    {
                        buttons[i][j] = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton("")
                        { CallbackData = "/-"};
                    }
                }
            }
            Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(buttons);
            return keyboard;
        }

        static internal Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup GetInlineTimeKeyboard()
        {
            return GetInlineKeyboard(
                    new InlineButton("00:00:00", "/t_00"),
                    new InlineButton("01:00:00", "/t_01"),
                    new InlineButton("02:00:00", "/t_02"),
                    new InlineButton("03:00:00", "/t_03"),
                    new InlineButton("04:00:00", "/t_04"),
                    new InlineButton("05:00:00", "/t_05"),
                    new InlineButton("06:00:00", "/t_06"),
                    new InlineButton("07:00:00", "/t_07"),
                    new InlineButton("08:00:00", "/t_08"),
                    new InlineButton("09:00:00", "/t_09"),
                    new InlineButton("10:00:00", "/t_10"),
                    new InlineButton("11:00:00", "/t_11"),
                    new InlineButton("12:00:00", "/t_12"),
                    new InlineButton("13:00:00", "/t_13"),
                    new InlineButton("14:00:00", "/t_14"),
                    new InlineButton("15:00:00", "/t_15"),
                    new InlineButton("16:00:00", "/t_16"),
                    new InlineButton("17:00:00", "/t_17"),
                    new InlineButton("18:00:00", "/t_18"),
                    new InlineButton("19:00:00", "/t_19"),
                    new InlineButton("20:00:00", "/t_20"),
                    new InlineButton("21:00:00", "/t_21"),
                    new InlineButton("22:00:00", "/t_22"),
                    new InlineButton("23:00:00", "/t_23"));
        }
    }
}
