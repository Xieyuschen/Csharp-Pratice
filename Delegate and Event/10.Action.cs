using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate_and_Event
{
    class Action
    {
        public class Stu<T>
        {
            public int Show(T s)
            {
                Console.WriteLine($"Hello world {s}");
                return 0;
            }
            public void Goodbye(T s)
            {
                Console.WriteLine($"Aufwiedersehen! {s}");
            }
        }
        static void Main()
        {
            Stu<string> stu = new Stu<string>();
            Action<string> action = new Action<string>(stu.Goodbye);
            action("\nIch liebe dich,Yuchen!");

        }
    }
}

