using System;
using System.Collections.Generic;
using System.Text;
//本次想直接把订阅在类中完成，而不是在Main函数中现场订阅。
//嗯然后我本来想是在state函数中的switch语句中完成对事件的订阅，但是这样并不可行故更换方法。

//那么就通过定义事件类的构造函数提前对事件进行订阅即可。
namespace Delegate_and_Event
{
    class 在事件类中完成订阅
    {
        public delegate void SayHiToYou();
        public delegate void Tschus();
        public class Publisher
        {
            public Publisher()
            {
                Relative relative = new Relative();
                ESayHiToYou = relative.Hello;
                ESayHiToYou += relative.GutenTag;
                ETschus = relative.Goodbye;
                ETschus += relative.Aufwiedersehen;
            }
            public event SayHiToYou ESayHiToYou;
           

            public event Tschus ETschus;
            
            public void State(string a)
            {
                Relative relative = new Relative();
                switch (a)
                {
                    case "Meet":
                        Console.WriteLine("A deutsch and an English meet");
                        ESayHiToYou();
                        break;

                    case "Leave":
                        Console.WriteLine("They say goodbye to each other");
                        ETschus();
                        break;

                    default:
                        Console.WriteLine("Nothing special happened");
                        break;
                }

            }
        }

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
        static void Main(string[] arg)
        {
            Relative relative = new Relative();
            SayHiToYou sayHiToYou = new SayHiToYou(relative.Hello);
            Publisher p = new Publisher();
            p.State("Meet");
            p.State("Leave");

        }
    }
}

