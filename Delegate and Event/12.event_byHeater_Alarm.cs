using System;
using System.Collections.Generic;
using System.Text;

//{

//    public class Heater
//    {
//        private int temperature;
//        private int temraisenum;
//        public string type = "RealFire001";
//        public string area = "China Xian";

//        public delegate void BoilEventHandler(Object sender, BoiledEventArgs e);
//        public event BoilEventHandler Boiled;

//        public class BoiledEventArgs : EventArgs
//        {
//            //这里这个类继承了EventArgs里面的Empty
//            public readonly int temperature;
//            public readonly int temraisenum;
//            public BoiledEventArgs(int temperature,int temraisenum)
//            {
//                this.temperature = temperature;
//                this.temraisenum = temraisenum;
//            }
//        }

//        protected virtual void OnBuiled(BoiledEventArgs e)
//        {
//            if (Boiled != null)
//            {
//                //委托BoilEventHandler的构造函数接受两个参数
//                //所以这里调用事件的时候也接受两个参数
//                Boiled(this, e);
//            }
//        }
//        public void BoilWater()
//        {
//            for (int i = 0; i < 101; i++)
//            {
//                temperature = i;
//                temraisenum =i;
//                if (temperature > 95)
//                {
//                    BoiledEventArgs e = new BoiledEventArgs(temperature,temraisenum);
//                    OnBuiled(e);
//                }
//            }

//        }
//    }
//    public class Alarm
//    {
//        public void MakeAlert(Object sender,Heater.BoiledEventArgs e)
//        {
//            Heater heater = (Heater)sender;
//            Console.WriteLine($"Alarm: {heater.area }--{heater.type}");
//            Console.WriteLine($"Alarm:嘀嘀嘀，水已经 {e.temperature }度了！");
//            Console.WriteLine($"Alarm:嘀嘀嘀，水已经加热 {e.temraisenum }次了！");
//            Console.WriteLine();
//        }
//    }
//    public class Display
//    {
//        public static void ShowMsg(Object sender,Heater.BoiledEventArgs e)
//        {
//            Heater heater = (Heater)sender;
//            Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
//            Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.temperature);
//            Console.WriteLine();
//        }
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            Heater heater = new Heater();
//            Alarm alarm = new Alarm();
//            Display display = new Display();
//            heater.Boiled += alarm.MakeAlert;
//            heater.Boiled += Display.ShowMsg;

//            heater.BoilWater();
//        }
//    }
//}
namespace Delegate_.net_pra
{
    public delegate void NumberChangedEventHandler(int count);
    public class Publisher
    {
        private int count;
        //类中的一个委托成员
        private event NumberChangedEventHandler NumberChanged;
        //public NumberChangedEventHandler NumberChanged;
        public void Register(NumberChangedEventHandler method)
        {
            NumberChanged = method;
        }
        public void UnRegister(NumberChangedEventHandler method)
        {
            NumberChanged -= method;
        }
        public void DoSomething()
        {
            if (NumberChanged != null)
            {
                ++count;
                NumberChanged(count);
            }
        }
    }
    public class Subscriber
    {
        public void OnNumberChanged(int count)
        {
            Console.WriteLine($"Subsriber notified: count = {count}");
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Publisher publisher = new Publisher();
            Subscriber subscriber = new Subscriber();
            publisher.Register(subscriber.OnNumberChanged);
            publisher.DoSomething();
        }
    }
}