using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate_and_Event
{
    class Class1
    {
        public delegate void Dosomething<T>(T t);
        public class Publisher<T>
        {
            public event Dosomething<T> Reminder;
            public void Show(T t)
            {
                Reminder(t);
            }
        }
        public class Gut<T>
        {
            public void Tag(T t)
            {
                Console.WriteLine($"It is morning now and Yuchen has say {t} to you!");
            }
        }

        static void Main()
        {
            Gut<string> gut = new Gut<string>();
            Dosomething<string> dosomething = new Dosomething<string>(gut.Tag);
            Publisher<string> publisher = new Publisher<string>();
            publisher.Reminder += dosomething;
            publisher.Show("Wie geht es ihnen?");
        }
    }
}
