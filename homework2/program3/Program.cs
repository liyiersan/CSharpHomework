using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(2 + " ");
            for (int i = 3;i <= 100; i++)
            {
                if (i%2 != 0 && i%3 != 0 && i%5 != 0 && i%7 != 0)
                {
                    Console.Write(i + " ");
                }
            }
        }
    }
}
