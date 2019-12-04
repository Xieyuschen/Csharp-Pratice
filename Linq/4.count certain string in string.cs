using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Linq
{
    class count_certain_string_in_string
    {
        static void Main()
        {
            string text = @"Hello today i like it,hello world,"+ @"hello world";
            string searchStr = "hello";
            //在分割字符串的时候，split遇到char[]中指定的字符时把这个字符忽略掉，存为一个字符串之后再开始存下一个
            string[] source = text.Split(new char[] {',',' '}, StringSplitOptions.RemoveEmptyEntries);
            var result = from item in source 
                         where item.ToLowerInvariant() == searchStr.ToLowerInvariant() 
                         select item;
            
            int n = result.Count();                                                                                                                                                             
            Console.WriteLine($"There are {n} same string {searchStr}.");
        }
    }
}
