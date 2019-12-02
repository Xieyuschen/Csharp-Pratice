using System;
using System.Collections.Generic;
/*
  嗯这是第一次尝试C#的接口使用，本来想是使用泛型但踩了一堆坑hhh
  首先写了两个接口：完成遇见时德语英语版的问好接口和离开时双语版的再见。
  然后全都是采用默认private方式去让Relative继承。然后使用在main函数中去运行

  这里比较突出的几点是：
       1.想要访问这些类方法不能够直接创建Relative对象然后点运算符，而是必须创建接口类然后用点运算符
       2.那么跟着就是对于接口类内部的默认似乎并不重要，因为无论在interface前加public或private限定符、
       都不影响我们创建类对象的时候访问。当然话又说回来其实写private的方法在接口里面是没有意义的。因
       为接口只管说明你要实现什么功能（这些功能都是用来使用的）。而private的方法没有必要放到接口里，
       接口才不管你怎么实现呢。
       3.这样写想要实现一个功能就必须创建一个接口对象，因为我在Relative类里实现的方法是[interface]
       .[FuncName],所以在访问的时候就必须放到接口类后面。
     */
namespace Delegate_and_Event
{
    class 接口继承的愚蠢
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
            void IMeeting.GutenTag()
            {
                Console.WriteLine("Guten Abend!");
            }
            void IMeeting.Hello()
            {
                Console.WriteLine("Good Evening!");
            }

            void IAway.Aufwiedersehen()
            {
                Console.WriteLine("Aufwiedersehen!");
            }
            void IAway.Goodbye()
            {
                Console.WriteLine("Goodbye!");
            }
        }
        static void Main(string[] args)
        {
            Relative relative = new Relative();
            //relative. 是点不出来方法的
            IMeeting sentences=new Relative();
            sentences.Hello();
            sentences.GutenTag();
            IAway away = new Relative();
            away.Goodbye();
            away.Aufwiedersehen();
        }
    }
}
