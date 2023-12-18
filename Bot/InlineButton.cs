using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsItGoingBot.Bot
{
    internal class InlineButton
    {
        internal string Name { get; set; }
        internal string CallbackData { get; set; }
        internal InlineButton(string name, string callbackData)
        {
            Name = name;
            CallbackData = callbackData;
        }
    }
}
