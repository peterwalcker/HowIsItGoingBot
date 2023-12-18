using HowIsItGoingBot.Bot;
using HowIsItGoingBot.Local;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bot = Telegram.Bot;

namespace HowIsItGoingBot
{
    public partial class HIIGBotDebug : Form
    {

        BackgroundWorker bw;
        const long testchat = 1580355734;
        const long testgroupt = -1001942504807;

        public HIIGBotDebug()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(bw is null)
            {
                bw = new BackgroundWorker();
                bw.DoWork += HowIsItGoingBot.Bot.BotWork.DoBotWork;
            }
            if (this.bw.IsBusy != true)
            {
                this.bw.RunWorkerAsync();
                button1.Text = "Stop";
            }
            else
            {
                this.bw.Dispose();
                button1.Text = "Start";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bot.Message.SendMessage(testgroupt, "Как делишки?",
                HowIsItGoingBot.Bot.Keyboard.GetInlineKeyboard(
                    new InlineButton("Лучше всех", "/best"),
                    new InlineButton("Так себе", "/worst")));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnTimer.OnTickEvent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MotivationPicture.GetPicture(MotivationPicture.PictureType.Regular);
        }
    }
}
