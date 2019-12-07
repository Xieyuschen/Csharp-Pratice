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
## 创建任务延续  
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
        //这里的参数x就是getData运行之后返回的values数组.
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
- 首先`GetData`通过调用`TaskFactory.StartNew<TRusult>(Func<TReslut>)`方法来启动.`getData`返回一个数组.  

- 然后我们来关注`ContinueWith(/*some important in parentnese*/)`  
现在我知道了`ContinueWith`是用来指定先行任务的,然后把先行任务的输出当作输入.那么我们来看`var processData = getData.ContinueWith((x) =>...)`这里面的内容,x作为一个传入参数,而在括号的代码框里,又出现了`x.GetUpperBound`这样的语句,说明x是一个数组,那么这个数组是怎么传进来的呢?很明显没有显式的指定传进来的参数,那么这个参数肯定是`ContinueWith`完成的.  
事情应该是在这样的,ContinueWith指定了先行任务,那么先行任务再执行完后会自动的把自己返回的结果传递给之后的任务进行执行.

- 关注最后一行的`displayData.Result`,这个语句为什么能让控制台输出一些内容呢?  
首先`Result`的代码提示为:
`Gets the result value of this Task<TResult>`  
也就是说这里调用了displayData,然后发现这个有一个先行任务,那么我需要执行先行任务,然后这样追踪到processData,因为getData已经返回来值,所以说就逐渐再倒回来进行运算.  
  
- `ContinueWith`的额外用法:  
因为 Task.ContinueWith 是实例方法，所以我可以将方法调用链接在一起，而不是为每个先行任务去实例化 `Task<TResult>` 对象。 以下示例与上一示例在功能上等同，唯一的不同在于它将对 Task.ContinueWith 方法的调用链接在一起。 请注意，通过方法调用链返回的 Task<TResult> 对象是最终延续任务。
```C#
 var displayData = Task.Factory.StartNew(() => { 
                                                 Random rnd = new Random(); 
                                                 int[] values = new int[100];
                                                 for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                                                    values[ctr] = rnd.Next();

                                                 return values;
                                              } ).  
                        ContinueWith((x) => {
                                        int n = x.Result.Length;
                                        long sum = 0;
                                        double mean;
                                  
                                        for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                                           sum += x.Result[ctr];

                                        mean = sum / (double) n;
                                        return Tuple.Create(n, sum, mean);
                                     } ). 
                        ContinueWith((x) => {
                                        return String.Format("N={0:N0}, Total = {1:N0}, Mean = {2:N2}",
                                                             x.Result.Item1, x.Result.Item2, 
                                                             x.Result.Item3);
                                     } );                         
      Console.WriteLine(displayData.Result);
```


嗯这两个方法的使用方法就是这样了,微软的文档真的是具体入微,超级棒!.^_^

## 创建分离的子任务:
- 如果在任务中运行的用户代码创建一个新任务，且未指定 AttachedToParent 选项，则该新任务不采用任何特殊方式与父任务同步。 这种不同步的任务类型称为“分离的嵌套任务” 或“分离的子任务”  
注意这里创建的任务是在一个任务中建立的,即嵌套任务.在当前任务的用户代码中再创建一个任务,然后不指定 选项就会是子任务.  
- 父任务不会等待分离子任务完成.  


# 异步编程
- I/O绑定与CPU绑定代码  
I/O绑定，如从网络请求的数据或访问数据库。CPU绑定代码，如执行成本高昂的计算。   
## 异步模型的基本概述
- `Task`,`Task<T>`,`async`与`await`.  
对于I/O绑定代码，当`await`一个操作，将返回async方法中的一个Task 或 `Task<T>`.对于CPU绑定代码，使用await代码时后台线程通过Task.Run启动。
`async`关键字把方法转换为异步方法，这样可以在正文使用`await`关键字。  
应用`await`关键字之后，它将挂起调用方法，并将控制权还给调用方（所以啥是调用方？！），直到等待的任务完成。并且仅允许在异步的方式中使用`await`，

## 识别CPU绑定和I/O绑定工作  
- 在绑定时要确定所执行的操作时I/O绑定或CPU绑定非常重要，因为这会极大影响代码性能。在写代码的时候可以根据这两个问题来确定使用哪种绑定：  
(1) 如果代码会等待某些内容，例如数据库的数据，那么工作为I/O绑定。  
(2) 如果代码会执行开销巨大的计算，则工作为CPU绑定。  
如果工作为I/O绑定，使用async和await就可以（而不使用Task.Run)。如果为CPU绑定，且**重视**响应能力，不仅使用async和await，而且在另一个线程上使用Task.Run生成工作。

- 阻塞与await  
阻塞最典型的例子就如学习编程最常见的顺序型执行代码，每执行一条语句都会阻塞，什么都不会做直到这一条语句被执行完。但这种方式显然是不合适的，所以使用await。await的功能是在执行一部分代码的时候，不会阻塞而会响应别的任务，但是如果只有await那也不会启动任何的其他任务。那微软Docs里的例子放这里：
```C#
static async Task Main(string[] args)
{
    Coffee cup = PourCoffee();
    Console.WriteLine("coffee is ready");
    Egg eggs = await FryEggs(2);
    Console.WriteLine("eggs are ready");
    Bacon bacon = await FryBacon(3);
    Console.WriteLine("bacon is ready");
    Toast toast = await ToastBread(2);
    ApplyButter(toast);
    ApplyJam(toast);
    Console.WriteLine("toast is ready");
    Juice oj = PourOJ();
    Console.WriteLine("oj is ready");

    Console.WriteLine("Breakfast is ready!");
}
```
在之前没有加await的时候，烤面包的操作就是把面放到烤箱里然后一直盯着，其他的任何事情都不会接受（被忽略）。而加上await之后虽然还是把面包放进去然后一直盯着，但是现在已经可以回应想引起注意的东西。

- 同时启动任务  
`System.Threading.Tasks.Task` 和相关类型是可以用于推断正在进行中的任务的类。要同时开始任务就要把所有任务都加到Task里面去，然后具体执行的时候根据各自完成特点进行执行。也就是说这些任务都是这个时候开始的（加入任务队列我觉得更加的贴切）。然后之后使用await让它们执行，这样的话可以一次启动所有的异步任务。 仅在需要结果时才会等待每项任务。 
- 与任务组合
下面这段代码很好的说明了一个异步方法应该如何定义和使用。
```C#
async Task<Toast> MakeToastWithButterAndJamAsync(int number)
{
    var toast = await ToastBreadAsync(number);
    ApplyButter(toast);
    ApplyJam(toast);
    return toast;
}
```
首先签名里具有`async`这个签名。这个签名会向编译器发出信号，说明该方法**包含`await`语句**，也包含异步操作。此方法返回一个`Task<TResult>`.  
所以可以通过将操作分离到一个**返回任务的新方法**来组合任务。可以选择等待此任务的时间，也可以同时启动其他的任务。
- 高效的等待任务  
可以使用`Task`类的方法改进上述代码末尾的一系列`await`语句。常见API有：`WenAll`,`WhenAny`.返回一个任务，一个是全部完成才返回，另一个是有一个完成就返回。