using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace program1
{
    public class AlarmEventArgs : EventArgs
    {
        public int seconds;//响铃持续的时间
    }
    //声明委托类型
    public delegate void AlarmEventHandler(Time sender, AlarmEventArgs e);

    //定义时间类
    public class Time
    {
        private int hour, minute, second;

        public Time(int h = 0, int m = 0, int s = 0) //构造函数
        {
            Hour = h;
            Minute = m;
            Second = s;
        }
        public int Hour
        {
            set
            {
                if (value >= 0 && value < 24)
                    hour = value;
                else 
                    throw new ArgumentOutOfRangeException();

            }
            get{ return hour;}
        }

        public int Minute
        {
            set
            {
                if (value >= 0 && value < 60)
                    minute = value;
                else
                    throw new ArgumentOutOfRangeException();

            }
            get { return minute; }
        }

        public int Second
        {
            set
            {
                if (value >= 0 && value < 60)
                    second = value;
                else
                    throw new ArgumentOutOfRangeException();

            }
            get { return second; }
        }
    }
    //定义闹钟类
    public class Clock
    {
        //声明事件
        public event AlarmEventHandler AlarmEvent;
        //当前时间
        private Time currentTime;//当前时间


        private Time alarmTime;//闹钟时间
        public Clock()
        {
            currentTime = new Time();          
        }
        public Clock(int h,int m,int s)
        {
            alarmTime = new Time(h, m, s);
        }
        public void Run(AlarmEventArgs args)
        {
            
            while (true)
            {
                currentTime = new Time(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (currentTime.Hour == alarmTime.Hour 
                    && currentTime.Minute == alarmTime.Minute
                    && currentTime.Second == alarmTime.Second)
                {
                    AlarmEvent(alarmTime, args);
                }
                Thread.Sleep(1000);
            }
        }     
    }

    public class Test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input your alarm time: ");
            int h, m, s,l;
            Console.WriteLine("Hour = ");
            h = int.Parse(Console.ReadLine());
            Console.WriteLine("Minute = ");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine("Second = ");
            s = int.Parse(Console.ReadLine());
            Console.WriteLine("Last Seconds = ");
            l = int.Parse(Console.ReadLine());
            AlarmEventArgs ae = new AlarmEventArgs();
            ae.seconds = l;
            var clocker = new Clock(h,m,s);
            clocker.AlarmEvent += Alarming;
            clocker.Run(ae);
        }
        static void Alarming(Time t, AlarmEventArgs e)
        {
            Console.WriteLine($"Alarm time -- {t.Hour}:{t.Minute}:{t.Second}");
            Console.WriteLine($"Contiue {e.seconds} seconds");
            while (e.seconds-- > 0)
            {
                Console.WriteLine("Alarming\a");
                Thread.Sleep(1000);
            }
        }
    }
}