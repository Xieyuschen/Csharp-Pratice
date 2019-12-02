using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate_and_Event
{
    class Class1
    {
        interface IMeeting
        {
            void Hello();
            void GutenTag();

        }
        interface IAway
        {
            void Goodbye();
            void Aufwiedersehen();
        }
        class Relative : IMeeting, IAway
        {
            public void GutenTag()
            {
                Console.WriteLine("Guten Abend!");
            }
            public void Hello()
            {
                Console.WriteLine("Good Evening!");
            }

            public void Aufwiedersehen()
            {
                Console.WriteLine("Aufwiedersehen!");
            }
            public void Goodbye()
            {
                Console.WriteLine("Goodbye!");
            }
        }

        static void Main(String[] arg)
        {
            Relative relative = new Relative();
            relative.Hello();
            relative.GutenTag();
            relative.Goodbye();
            relative.Aufwiedersehen();
        }
    }
}
