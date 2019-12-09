using System;

namespace ConsoleApp1
{
    class Program
    {
        public delegate void DoSomeThing();
        //目前op是一个对象，我们可以假设这是一个控件类。
        public class Op
        {
            //类中有一个事件叫做SayEvent。
            public event DoSomeThing SayEvent;
            public Op()
            {
                DoSomeThing doSomeThing = new DoSomeThing(() => { Console.WriteLine("Hello World"); });
                SayEvent += doSomeThing;
            }
            public void click()
            {
                SayEvent();
            }
        }
        static void Main(string[] args)
        {
            Op op = new Op();
            op.click();
        }
    }
}
