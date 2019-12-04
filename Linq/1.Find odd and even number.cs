using System;
using System.Linq;
namespace Linq_pratice
{
    class 数组查找偶数
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var Evennum = numbers.Where(num => (num % 2) == 0);
            var Oddnum = from num in numbers where (num % 2 == 1) select num;

            Console.WriteLine("The Jishu in array numbers are:");
            foreach (var item in Oddnum) Console.Write($"{item} ");
            Console.WriteLine();
            Console.WriteLine("The Oishu in array numbers are:");
            foreach (var item in Evennum) Console.Write($"{item} ");

        }
    }
}
