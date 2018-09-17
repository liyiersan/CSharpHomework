using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program2
{
    class Program
    {
        static void Main(string[] args)
        {
            int []nums = new int[5] {45, 34, 56, 2, 8};
            int max, min,sum;
            max = min = sum = nums[0];
            for(int i = 0; i < 4; i++)
            {
                sum += nums[i + 1];
                if (min > nums[i + 1])
                    min = nums[i + 1];
                if(max < nums[i + 1])  
                    max = nums[i + 1];  
            }
            double avg = sum / 5.0;
            Console.WriteLine("max = " + max + " min = " + min + " sum = " + sum + " avg = " + avg);
        }
    }
}
