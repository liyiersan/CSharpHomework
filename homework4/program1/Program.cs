using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace program1
{
    public class ClockEventArgs : EventArgs
    {

    }

    public delegate void ClockEventHandle(object obj, ClockEventArgs Args);//委托声明

    public class Clock
    {
        public event ClockEventHandle Clocking;//声明事件
        public void DoClock()
        {
            if (Clocking != null)
            {
                ClockEventArgs args = new ClockEventArgs();
                Clocking(this, args);//每触发一次事件，通知一次外界
            }
        }
    }

    class program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Please input your hour：");
            string h = Console.ReadLine();
            while (true)
            {
                try
                {
                    while (Int32.Parse(h) > 23 || Int32.Parse(h) < 0)
                    {
                        Console.WriteLine("Error, please input again：");
                        h = Console.ReadLine();
                    }
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error, please input again：");
                    h = Console.ReadLine();
                }
            }
            Console.WriteLine("Please input your minute: ");
            string m = Console.ReadLine();
            while (true)
            {
                try
                {
                    if (m.Length == 1)
                        m = "0" + m;
                    while (Int32.Parse(m) > 59 || Int32.Parse(m) < 0)
                    {
                        Console.WriteLine("Error, please input again：");
                        m = Console.ReadLine();
                    }
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error, please input again：");
                    m = Console.ReadLine();
                }
            }
            string s = h + ":" + m;


            var clock = new Clock();
            string now_t = DateTime.Now.ToShortTimeString().ToString();
            Console.WriteLine("Current time：" + now_t);
            while (now_t != s)
            {
                Thread.Sleep(60000);
                now_t = DateTime.Now.ToShortTimeString().ToString();
                Console.WriteLine("Current time：" + now_t);
            }
            clock.Clocking += Ring;
            clock.DoClock();
        }
        static void Ring(object sender, ClockEventArgs e)
        {
            Console.WriteLine("Clocking\a\a\a\a\a\a\a\a\a\a\a\a\a\a");
        }

    }
}