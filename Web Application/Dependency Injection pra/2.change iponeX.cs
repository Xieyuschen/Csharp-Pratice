using System;
/*
Ming lost his iphone6 and bought the iphoneX,and he still want to do such thing like reading and playing.
     */
namespace Dependency_Injection_pra
{
    class Program
    {
        interface Iiphone
        {
            void read(string name);
            void play(string name);
        }
        //public class Iphone6 : Iiphone
        //{
        //    public void read(string name)
        //    {
        //        Console.WriteLine($"{name} open the Zhihu and compose a fake story to earn likes");
        //    }
        //    public void play(string name)
        //    {
        //        Console.WriteLine($"{name} plays games as a green finger");
        //    }
        //}

        //the first place to change!
        public class IphoneX : Iiphone
        {
            public void read(string name)
            {
                Console.WriteLine($"{name} open the Zhihu and compose a fake story to earn likes");
            }
            public void play(string name)
            {
                Console.WriteLine($"{name} plays games as a green finger");
            }
        }

        //the second place to change
        //there are four iphone6 have to be changed to iphoneX.
        //this just two method and it's very complex to change,what about more than 10 func happening?
        public class Person
        {
            public Person(string a)
            {
                name = a;
                age = 20;
                //there are no more Iphone6 but IphoneX
                iphoneX = new IphoneX();
            }
            public string name { get; set; }
            private int age { get; set; }
            private IphoneX iphoneX;
            public void play()
            {
                iphoneX.play(name);
            }
            public void read()
            {
                iphoneX.read(name);
            }

        }
        static void Main(string[] args)
        {
            Person person = new Person("Ming");
            person.read();
            person.play();
        }
    }
}
