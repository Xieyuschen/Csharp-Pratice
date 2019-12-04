using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate_and_Event
{
    //在使用泛型委托的时候，委托类和要被委托的类后面要加上<T>表示这是一个泛型类，而包含这两个类的类是不需要包含泛型说明符的
    public class generic_delegate
    {
        delegate void Dosomething<T>(T temp);
        public class TrafficLight<T>
        {
            public T state;
            public void Show(T t)
            {
                Console.WriteLine($"Suprising! This is {t}");
            }
        }
        static void Main()
        {
            TrafficLight<int> trafficlight = new TrafficLight<int>();
            Dosomething<int> dosomething = new Dosomething<int>(trafficlight.Show);
            dosomething(4);
            TrafficLight<string> trafficLight1 = new TrafficLight<string>();
            Dosomething<string> dosomething1 = new Dosomething<string>(trafficLight1.Show);
            dosomething1("Hello world");
        }
    }
    
}
