using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input an integer:");
            int num, count = 0;
            num = int.Parse(Console.ReadLine());
            for(int i = 2; num / i > 0;)
            {
                if (num % i == 0)
                {
                    num /= i;
                    Console.Write(i + " ");
                    count++;
                    if (count % 10 == 0)
                        Console.WriteLine();
                }
                else
                    i++;
            }
            Console.WriteLine();




        }
    }
}
