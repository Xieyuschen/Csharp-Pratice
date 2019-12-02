using System;
using System.Collections.Generic;
using System.Text;
//事件（Event）配合委托可以完成函数调用随着事件触发而触发
//那么这里就实现一个功能，创建两个event，当第一个event触发的时候实现问好功能
//第二个event触发的时候实现告别，同时两个人在相遇结束时候都会在控制台输出文字
//显示
namespace Delegate_and_Event
{
    class Class1
    {
        public delegate void SayHiToYou();
        public delegate void Tschus();
        public class Publisher
        {
            public event SayHiToYou ESayHiToYou;
            protected virtual void RaiseESayHiToYou()
            {
                ESayHiToYou();
            }

            public event Tschus ETschus;
            protected virtual void RaiseETschus()
            {
                ETschus();
            }
            public void State(string a)
            {

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
            p.ESayHiToYou += sayHiToYou;
            p.ESayHiToYou += relative.GutenTag;      
            p.ETschus += relative.Aufwiedersehen;
            p.ETschus += relative.Goodbye;
            p.State("Meet");
            p.State("Leave");

        }
    }
}
