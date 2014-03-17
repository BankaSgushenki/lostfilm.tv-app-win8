using System;
using Windows.UI.Xaml;

namespace lostfilm.tv_app_win8.Logic
{
    class Timer
    {
        public static DispatcherTimer timer = new DispatcherTimer();

        public static void setTimer(int timerPeriod)
        {
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00, 0, timerPeriod);
            timer.Start();
        }

        public static void timer_Tick(object sender, object e)
        {
            StartClass.start("http://www.lostfilm.tv");
        }
    }

}
