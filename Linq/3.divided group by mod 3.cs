using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Linq
{
    class Class1
    {
        static void Main()
        {
            int[] arr = { 1, 2, 3, 9, 8, 7, 5, 6, 4 };
            var threegroup = from item in arr group item by item % 3;
            foreach(var p in threegroup)
            {
                Console.Write(p.Key==0?"\nFull divided by 3:\n":(p.Key==1?"\nDivided by 3 but left 1:\n": "\nDivided by 3 but left 2:\n"));
                //Console.Write($"{p} ");
                foreach(var item in p)
                {
                    Console.Write($"{item} ");
                }
            }

            var threegroup2 = arr.GroupBy(item => item % 3);
            foreach (var p in threegroup2)
            {
                Console.Write(p.Key == 0 ? "\nFull divided by 3:\n" : (p.Key == 1 ? "\nDivided by 3 but left 1:\n" : "\nDivided by 3 but left 2:\n"));
                //Console.Write($"{p} ");
                foreach (var item in p)
                {
                    Console.Write($"{item} ");
                }
            }
        }
    }
}
