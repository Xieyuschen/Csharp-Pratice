using System;
/*
 this pratice is follow the article provided by the Zhihu,
 https://www.zhihu.com/question/32108444
 ja,it's pretty good and lively,even using the language i'm not fimliar to.
 so i want to rewrite it by the C# to understand it better!

in this part the person have the ipone6 and crab it everytime.
so what happened if his ipone6 lost and he bought an iphoneX.
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
        public class Iphone6 : Iiphone
        {
            public void read(string name)
            {
                Console.WriteLine($"{name }open the Zhihu and compose a fake story to earn likes");
            }
            public void play(string name)
            {
                Console.WriteLine($"{name } plays games as a green finger");
            }
        }

        public class Person{
            public Person(string a)
            {
                name = a;
                age = 20;
                iphone6 = new Iphone6();
            }
            public string name { get; set; }
            private int age { get; set; }
            private Iphone6 iphone6;
            public void play()
            {
                iphone6.play(name);
            }
            public void read()
            {
                iphone6.read(name);
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
