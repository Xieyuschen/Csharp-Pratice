using System;
using System.Collections.Generic;
using System.Text;
/*
   现在我觉得我双语每次都要点两次有点麻烦，我想使用一个方法一次就可以调用两个
   那么这时候就可以使用到委托这个功能
     */
namespace Delegate_and_Event
{
    class 使用委托
    {
        //委托如果相对一个类使用，是要声明在使用类的外边的。声明在内部是用不了的
        //你看这里SayToYou 是一个类对象的颜色，就是因为他会隐式创建一个对象。
        //知道这一点就可以了
        public delegate void SayToYou();

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
        class try1{
            public void Show()
            {
                Console.WriteLine("delegate different class is right");
            }
        }
        static void Main(string[] arg)
        {
            Relative relative = new Relative();
            try1 try1 = new try1();
            //添加一个对象的方法（必须实例化）并且不需要在添加的时候带函数参数列表
            SayToYou sayToYou = new SayToYou(relative.Hello);
            sayToYou += relative.GutenTag;
            sayToYou += relative.Goodbye;
            sayToYou += relative.Aufwiedersehen;
            sayToYou += try1.Show;
            //调用委托的时候可以直接加括号就可以了
            sayToYou();

        }
    }
}
