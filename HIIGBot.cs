using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HowIsItGoingBot
{
    public partial class HIIGBot : ServiceBase
    {
        BackgroundWorker bw;
        System.Timers.Timer timer;
        public HIIGBot()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (bw is null)
            {
                bw = new BackgroundWorker();
                bw.DoWork += HowIsItGoingBot.Bot.BotWork.DoBotWork;
            }
            if (this.bw.IsBusy != true)
            {
                this.bw.RunWorkerAsync();
            }
            timer = new System.Timers.Timer();
#if DEBUG
            timer.Interval = 60000;
#else
            timer.Interval = 600000;
#endif
            timer.Enabled = true;
            timer.Elapsed += timer1_Tick;
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bot.OnTimer.OnTickEvent();
        }

    }
}
