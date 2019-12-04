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
            string[] strs = { "the", "quick", "brown", "fox", "jumps" };
            //按照字符的长短进行升序排序
            //默认为升序排列，descending为降序排列
            //有两种排序的方式，
            var sortbylen = from item in strs orderby item.Length select item;
            var sortbylen2 = from item in strs orderby item.Length descending select item;
            //如果需要指定次要排列规则的时候，仍要指定排序的对象是什么。
            var sort3 = from item in strs orderby item.Length, item.Substring(0, 1) select item;
            var sort4 = from item in strs orderby item.Length descending, item.Substring(0, 1) descending select item;
            Console.WriteLine("default sorted:");
            foreach (var item in sortbylen){
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n\nReserve sorted:");
            foreach (var item in sortbylen2)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n\nSort by len and alphabyte:");
            foreach (var item in sort3)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n\nSort by discending len and discending alphabyte:");
            foreach (var item in sort4)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            var othsort1 = strs.OrderBy(item => item.Length).ThenBy(item => item.Substring(0, 1));
            var othsort2 = strs.OrderByDescending(item => item.Length).ThenByDescending(item => item.Substring(0, 1));

            Console.WriteLine("\n\nSort by len and alphabyte by using dort operator:");
            foreach (var item in othsort1)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("\n\nSort by discending len and discending alphabyte by using dort operator:");
            foreach (var item in othsort2)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
