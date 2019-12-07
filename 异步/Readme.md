# 12.6 异步操作的学习
## 隐式创建和运行任务
`Parallel.Invoke`提供了可同时使用任意数量语句的方式。
——给每个工作项传入Action委托，可以使用lambda表达式来完成（嗯但是我不清楚这里是如何的把lambda表达式隐式的指示为Action委托的）。Microsoft Docs给出的代码如下：
```C# 
Parallel.Invoke(() => DoSomeWork(), () => DoSomeOtherWork());
```
嗯大概可能是在Invoke这个函数里面的表达式就会自动的成为委托嘛？嗯这个问题好像比较复杂，但不是当前学习的主要操作所以就先把问题留在这里。（逃了逃了

## 显示创建和运行任务
不返回值的任务由`System.Threading.Tasks.Task`表示  
返回值的任务由`System.Threading.Tasks.Task<Tresult>`表示  
创建任务的时候可以给任务挂一个委托（这个委托里面封装了任务要执行的代码）。委托可以是命名的委托，匿名方法或者lambda表达式。嗯这里的语句可以看出lambda表达式是可以完成委托的嗯这个问题等到我熟练应用lambda表达式的时候一个就没有什么问题了。  
然后第一个程序即对action委托以及lambda表达式（其实是例子中有的不是自己写的）进行立案西。
- Task.Start()、 Run()、以及Taskfactory.StartNew().
- 1. Start方法可以首先创建一个Task任务，然后使用点运算符可以点出来进行使用。
- 2. Run方法只能`Task task1=new Task.Run(//some functions);`
使用 Task.Run 方法通过一个操作创建并启动任务。 无论是哪个任务计划程序与当前线程关联，Run 方法都将使用默认的任务计划程序来管理任务。 不需要对任务的创建和计划进行更多控制时，首选 Run 方法创建并启动任务。
- 3. 使用 TaskFactory.StartNew 方法在一个操作中创建并启动任务。 **不必将创建和计划分开**并且需要其他任务创建选项或使用特定计划程序时，或者需要将其他状态传递到可以通过 Task.AsyncState 属性检索到的任务时，请使用此方法。

**所以一个任务是如何接受一个委托的？**
嗯让我来试一试。那么很简单就是这么实现的：
```C#
    Action action = new Action(myFavour.SayHello);
    action += myFavour.SayGoodbye;
    Task TaskDelegate = new Task(action);
```  
Task接受一个Action类型的委托可以这么写。

- 后续：  
lambda表达式在这一块是相当重要的，之后学了再说吧。

异步最大的特点我觉得是我只保证在这一段时间内任务都会被执行，但是不确定谁先谁后。例如
```C#
   Learn learn = new Learn();
   Action action = new Action(learn.Show);
   Task task = new Task(action);
   task.Start();
   Console.WriteLine("Guten Tag!");
   task.Wait();
```
这段代码Show函数会输出Goodmorning，在异步过程中可能先输出gutentag，也有可能先输出goodmorning。这个谁先谁后是不确定的，这也就是异步的思路：在执行一些任务的时候是可以执行其他事情。


# 12.7 
- 任务,线程与区域性   
每一个现场都具有一个关联的区域和UI属性,分别由Threading.CurrentCulture 和 Threading.CurrentUICulture 属性定义.  
线程的区域用于注入格式,分析,排序和字符串比较操作中. 线程的UI属性用于查找资源.CultureInfo.DefaultThreadCurrentCulture 和CultureInfo.DefaultThreadingCurrentUICulture 属性可以为所有线程指定默认区域性.如果不使用这两个属性修改,系统区域性为我们定义线程的默认区域性和UI属性.
- 创建任务延续  
使用`Task.ContinueWith` 和 `Task<TResult>.ContinueWith` 方法,可以指定**要在先行任务完成时启动**的任务.延续任务的委托已经传递了对先行任务的引用,因此可以检查先行任务的状态.并可以通过检索 `Task<TResult>.Result`属性的值将先行任务的输出用作延续任务的输入.  
简单而言,就是可以使用一个方法将任务A绑定到任务B上面去,任务A只有在B执行完之后才会被执行,这个步骤的完成就靠上面说的那几个方法和属性.
```C#
var getData = Task.Factory.StartNew(() => {
            Random rnd = new Random();
            //give array the random value
            int[] values = new int[100];
            for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                values[ctr] = rnd.Next();

            return values;
        });
        var processData = getData.ContinueWith((x) => {
            int n = x.Result.Length;
            long sum = 0;
            double mean;

            for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                sum += x.Result[ctr];

            mean = sum / (double)n;
            return Tuple.Create(n, sum, mean);
        });
        var displayData = processData.ContinueWith((x) => {
            return String.Format("N={0:N0}, Total = {1:N0}, Mean = {2:N2}",
                                 x.Result.Item1, x.Result.Item2,
                                 x.Result.Item3);
        });
        //Result将先行输出作为输入处理,那在这里即
        Console.WriteLine(displayData.Result);
```
嗯这里给出了创建任务延续的例子,关注如何通过方法`ContinueWith()`来完成任务的延续.  
首先`GetData`通过调用`TaskFactory.StartNew<TRusult>(Func<TReslut>)`方法来启动.`getData`返回一个数组.  
关注最后一行的`displayData.Result`,这里说明`Result`指明要把`displayData`的先行任务的输出作为这里displayData的输入.那么我就需要去寻找`ContinueWith()`这个方法,这个方法知道你过的就是当前需要寻找的.  
那么阅读代码,我发现一句  
` var displayData = processData.ContinueWith()`  
那么这个式子是什么意思呢? ---- 即把`displayData`的先行任务指定为`processData`.  
那么这里可以理解了,但是
