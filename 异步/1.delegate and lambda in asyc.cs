using System;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
    public class MyFavour
    {

        public void SayHello()
        {
            Console.WriteLine("Good Afternoon!");
        }
        public void SayGoodbye()
        {
            Console.WriteLine("GoodBye!");
        }
    }
    public static void Main()
    {
        Thread.CurrentThread.Name = "Main";

        Task TaskLambda = Task.Run(() => Console.WriteLine("Hello from taskA."));

        MyFavour myFavour = new MyFavour();
        Action action = new Action(myFavour.SayHello);
        action += myFavour.SayGoodbye;
        Task TaskDelegate =Task.Run(action);

        Console.WriteLine($"Hello from thread '{Thread.CurrentThread.Name}'.");

        TaskLambda.Wait();
        TaskDelegate.Wait();
    }
}
// The example displays output like the following:
//Good Afternoon!
//Hello from taskA.
//Hello from thread 'Main'.
//GoodBye!
//很明显看出来异步在输出的时候结果并不一定是确定的，可能在没有声明异步的前、后进行输出。
//Hello from taskA.
//Hello from thread 'Main'.
//Good Afternoon!
//GoodBye!