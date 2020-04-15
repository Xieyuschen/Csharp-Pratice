using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace 异步练习
{
    class Class1
    {
        public static void Clear(Object state)
        {
            Console.Clear();
        }
        public static void ShowTime(Object state)
        {
            Console.WriteLine($"{state.ToString()} {DateTime.Now.ToString("HH:MM:ss")}");
        }
        public static void Main()
        {

            TimerCallback timerCallback = new TimerCallback(Clear);
            timerCallback += ShowTime;
            Timer timer = new Timer(timerCallback, "timer", 0, 1000);
            Console.ReadKey();

        }

    }
}
