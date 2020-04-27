# 争用条件
&emsp;&emsp;这个例子很简单，写了两个类。  
&emsp;&emsp;第一个类`StateObject`只做一件事，把自己里面的初始值为5的数据成员递增1，如果这个成员的值为7就输出helloworld,然后在方法的最后再把数据成员的值设为5.  
那么问题来了，看样子helloworld好像永远不能输出啊？——这里就要用争用条件来演示helloworld的输出。
&emsp;&emsp;第二个类为`RaceCondition`，看名字就知道这个类里完成了争用条件。  

```cs
public class Net
{
    public class StateObject
    {
        private int _state = 5;
        public void ChangeState(int loop)
        {
            if (_state == 5)
            {
                ++_state;
                if (_state == 7)
                {
                    Console.WriteLine("helloworld");
                }
                //Trace.Assert(_state == 6, $"Race condition occurred after {loop} loops");
            }
            _state = 5;
        }
    }
    public class SampleTask
    {
        public void RaceCondition(object o)
        {
            Trace.Assert(o is StateObject, "o must be a StateObkect");
            StateObject stateObject = o as StateObject;
            int i = 0;
            while (true)
            {
                stateObject.ChangeState(i++);
            }
        }
        public void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 100; i++)
            {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }
        }
    }
    public static void Main()
    {
        var s = new SampleTask();
        s.RaceConditions();
        Console.ReadLine();
    }
}
```

## 输出：
当然是不停输出的helloworld了。因为此处的循环是无限的，所以就在无限竞争永远不停。

# 如何解决争用条件——>使用lock锁
当多个线程共享资源的时候，使用lock锁住，在一个线程中使用完解锁之后共享资源才会被下一个线程所使用。  
lock锁的时候必须是一个引用类型，所以常常创建一个`object`类型对象来进行管理。
```cs
public class StateObject
{
    //记得要new一下。
    object s_lock=new object();
    private int _state = 5;
    public void ChangeState(int loop)
    {
        lock (s_lock)
        {
            if (_state == 5)
            {
                ++_state;
                if (_state == 7)
                {
                    Console.WriteLine("helloworld");
                }
                //Trace.Assert(_state == 6, $"Race condition occurred after {loop} loops");
            }
            _state = 5;

        }
    }
}
```

# 死锁——>使用锁可能经常出现的东西。
只要设计的好，次次上锁出死锁，设计的不好发生死锁是必然的。比如同路径下的`DeadLock.cs`中的代码在执行的时候出现死锁的时机是不一样的，但是是一定会出现死锁的，在执行若干次之后就进入死锁了。

# 多个任务对一个对象进行操作，必须上锁否则将达不到预期效果：
这里对一个资源`ShareState`进行操作，如果是单个任务的话不会出现问题，因为只有一个线程在操作对象的资源。但如果是多线程的时候不对操作的资源上锁就会发生争用条件，然后得不到预期的结果。
```cs
public class Job
{
    private SharedState _sharedState;

    public Job(SharedState sharedState)
    {
        _sharedState = sharedState;
    }

    public void DoTheJob()
    {
        for (int i = 0; i < 50000; i++)
        {
            //对资源上锁之后多线程才能够得出正确的答案
            lock (_sharedState)
            {
                _sharedState.State += 1;
            }
        }
    }
}
```

# 线程pro
因为多线程执行的顺序并不固定，那么想要看一看输出的顺序是什么（虽然顺序也不确定，但就是想看）。改了一下代码，修改部分如下：
```cs

for (int i = 0; i < numTasks; i++)
{
    //Creates a task that will complete after a time delay.
    //在100ms之后创建了一个task，并完成
    Task.Delay(100).Wait();
    tasks[i] = Task.Run(() => new Job(state).DoTheJob(i));
}
        public void DoTheJob(int j)
{
for (int i = 0; i < 50000; i++)
{
    lock (_sharedState)
    {
        _sharedState.State += 1;
    }
    WriteLine(j);
}
```
那么呢，就出问题了，最后输出了20个20，也就是说在循环里传入`DoTheJob`函数的参数值全部为20.

## 导致问题的原因：
因为i在这个作用域中是一个局部变量，创建一个线程以及创建一个新对象是需要时间的，而在创建完毕之后循环已经进行了好几轮（甚至进行完了），此时函数接收的构造函数参数已经随着循环而改变了。本例中i在创建完线程和对象之后已经变成了20，所以说所有`DoTheJob`接收的参数均为20.  
解决办法很简单，第一个方法就是创建对象以及线程成功前不进入下一轮循环，当然这就涉及到一个问题就是这就没有体现到多线程的用途了，因为等待创建完对象的这段时间可能使用单线程就已经执行完了。另一种方法就是声明另外一个变量来保存i的值，这个值在超出此轮循环就已经失效，不会因为重新赋值导致的所有值都一样。  
```cs
for (int i = 0; i < numTasks; i++)
{
    int j = i;
    tasks[i] = Task.Run(() => new Job(state).DoTheJob(j));
}
```

## 想要看调用顺序的办法：
### 方法1：
&emsp;&emsp;问题在加入`Task.Delay(100).Wait()`后解决了，但是输出了1-20的顺序数字。  
### 方法2：
```cs
tasks[i] =new Task(t=> new Job(state).DoTheJob((int)t),i);
//等待直到tasks[i]完成，什么意思呢，也就是说会等待这个任务的委托内容完成再进行下面的操作。
//Wait会阻塞线程直到当前线程上的任务完成。
tasks[i].Start();
```
即Task接受一个func和一个输入参数，再调用`Start`参数就可以了。  

如果这样的话，我们在`DoTheJob`方法循环结束后加一条语句`WriteLine(_sharedState.State);`后，输出结果如下：
```
50000
0
//...
1000000
19
```