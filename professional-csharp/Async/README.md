# 异步
如何理解异步呢？首先强调一点，**异步和线程关系不大**，不要看到异步就想起多线程的问题，他们中间是有点差别的。  
## MSDOCS里面的刷盘子
[点这里](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/)  
这个教程中，很好地说明了什么是异步，什么是多线程。  
&emsp;&emsp;在做早餐的时候，烧水的时候去烤面包就是异步。这是一个真异步，因为烧水不是我（这里我可以理解为一个线程，而因为早餐是我一个人做的，所以是单线程）用用爱烧水的，而是用茶壶烧水的。这里的茶壶可以认为是一个应用程序，如OS的IO或者对数据库的查询。在这些事情做的时候，可以使用异步。就好比烧水的时候我不需要时刻盯着，我可以去做别的事情。查询数据库也可以使用异步进行，在这个过程中可以进行一些其他的操作。


# 一个小例子：
```csharp
public class Net
{
    public static async void ShowWithBlock(string str)
    {
        Task.Delay(10).Wait();
        Console.WriteLine(str);
    }
    public static async void Show(string str)
    {
        await Task.Delay(10);
        Console.WriteLine(str);
    }

    public static void Main()
    {
        //1.调用阻塞线程的操作：
        ShowWithBlock("hello");
        //2.调用await不阻塞线程的方法
        Show("hello");
        //3.正常输出的world
        Console.WriteLine("world");
    }
}
```

## 1. 调用1与3的Main方法：  
输出`hello world`在控制台界面。  
&emsp;&emsp;因为`Task.Delay(10).Wait()`会**阻塞当前的线程**。那么什么是阻塞当前的线程呢？——即在会在这里等待，直到线程阻塞结束之后才能进行别的东西，所以说在Wait结束之后，进行了`hello`的输出。

## 2. 调用2与3的Main方法：
输出`world hello`：  
&emsp;&emsp;因为await关键字并不阻塞线程，当然关于await的关键字将之后的代码作为`Task.ContinueWith()`的参数传入，之后的代码在阻塞线程没有释放的时候不会执行这些代码。

# 上面小栗子 pro
```csharp
public class Net
{
    public static async void Show(string str)
    {
        await Task.Delay(1000);
        Console.WriteLine("\n\n"+str+'\n');
    }
    public static void Main()
    {
        Show("hello");
        for(int i = 0; i < 10000; i++)
        {
            Console.Write(i.ToString()+" ");
        }
    }
}
```

## 输出结果：
```
4086 4087 4088 4089 4090 4091 4092 4093 4094 4095 4096 4097 4098 4099 4100 4101 4102 4103 4104 4105 4106 4107 4108 4109 4110 4111 4112 4113 4114 4115 4116

hello

4117 4118 4119 4120 4121 4122 4123 4124 4125 4126 4127 4128 4129 4130 4131 4132 4133 4134 4135 4136 4137 4138 4139 4140 4141 4142 4143 4144 4145 4146 
```
输出的情况就是如此：  
在运行时，await使线程不被阻塞，那么在执行`Task.Delay`的时候线程未被阻塞，所以可以进行一些其他的操作，当async的方法被执行完之后，将向消息队列里发出一条消息，然后将线程的控制交还给async方法，然后执行完后再返回之前执行的地方继续执行程序。  

- 那么对于线程来说会是怎么样的呢？
# 小栗子pro+
&emsp;&emsp;对于本程序呢，主要需要观察的是究竟什么叫做未阻塞线程。本程序的输出结果如下，输出结果表明异步使用了相同的线程。也即异步比多线程的概念更为宽泛(overlap).  
如果本例中将线程阻塞，`Task.Delay(100).Wait()`，那么就会按顺序一步步的进行输出，但仍然使用同一个线程。
```cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using System.Threading;

namespace Advance_Csharp
{

    public class Net
    {
        public static async void Show(string str)
        {
            Console.WriteLine("Line18: " + Thread.CurrentThread.ManagedThreadId.ToString());
            await Task.Delay(300);
            Console.WriteLine("\n\n"+str+'\n');
        }
        public static void Main()
        {
            Show("hello");
            Console.WriteLine("Line25: "+Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine("world");
            Console.WriteLine("Line27: " + Thread.CurrentThread.ManagedThreadId.ToString());

            for (int i = 0; i < 3000; i++)
            {
                Console.Write(i.ToString() + " ");
            }
            Console.WriteLine("\nLine33: " + Thread.CurrentThread.ManagedThreadId.ToString());

        }
    }
}
```

# 输出结果：
```
Line18: 1
Line25: 1
world
Line27: 1
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 
2190 2191 2192 2193 2194 2195 2196 2197 2198 2199 2200 2201

hello

2202 2203 2204 2205 2206 2207 2208 2209 2210 2211 2212
2994 2995 2996 2997 2998 2999
Line33: 1
```
