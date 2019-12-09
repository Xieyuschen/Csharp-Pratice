# 12.8开始学习
- 序  
嗯大概有两三个星期没有碰WPF，现在忘得是一干二净啥都不知道的。别人是改的内容亲妈都认不出来了，我是知识忘得自己都认不出来了。当时是从一个个的控件的用法学起来的，这样的好处就是我能很快的学会很多控件的使用方法能很好地反馈自己。但缺点就是我不懂原理然后忘得那叫一个快。。。所以这一遍就好好的把原理看一看这样效果会更好一点。  

## 关于拖一个控件之后双击生成方法中的参数：
在用图形界面拉出来一个控件之后，双击就会进入一个方法中，这个方法的签名已经被指定好了。对于简单的按钮控件，会生成以下代码：  
```C#
private void click1(object sender, RoutedEventArgs e)
        {
            //lbl1.Content = "Hello world";
        }
```


对于一个按扭键的property里面，会有两种界面。  
- Properties for the selected element  
在这个属性里面，主要设置一些关于外观以及其他的一些常用属性。  
- EventHandler for the selected element  
顾名思义，在这个属性见面里面我们可以对这个控件附加的一些实践进行命名操作与调用。控件是一个类，然后相当于在这个控件类中声明一个事件。如果在事件的属性中对一个事件命名，那就可以认为我可以通过这个名字来找到这个事件。当然一旦对其中某一个事件进行命名，在`MainWindow .xaml.cs`这个文件中会自动出现一个函数，函数名恰好与之前的命名相同。


## 通过比较手写事件和自动生成的委托理解原理。
- 首先对于系统自动生成的类   
当对窗口的property中click事件进行设置名称之后，在`MainWindow.xaml.cs`文件中有以下代码：
```C#
public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void click1(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "Hello world";
        } 
    }
```
MainWindow这个类很明显说的就是写出来的窗口这个类，这个毫无疑问。  
然后这个类里面有一个名字为`click1`的方法？——嗯？这不是我给click事件起的名字嘛？  
运行之后再点一下button，嗯为什么labal输出了hello world？  
这个就需要好好的想一想了。这个时候就要类比到控制台程序中去，介绍如下：
- 为了这个目的，专门书写了一个控制台程序，源代码如下：   
```C#
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
```
嗯看这个代码与上面WPF不同的地方有几点：
1. 控制台中的类Op拥有显式的构造函数，而WPF的MainWindow采用的是默认的构造函数。
2. 控制台程序想要输出必须要在Main函数中调用Op类的方法，而MainWindow类不需要。

- 下面就根据这两点的不同来探讨不同的形式来完成相同的功能。  
首先MainWindow中，如果点击了button按钮，那么点击按钮的事件click会被触发，事件被触发，所以在绑在事件上面的函数（或者说委托，而委托又是绑了很多函数的）会被执行。  
- 在控制台里click函数（相当于触发事件的条件）被调用的时候会自动调用事件，然后事件调用加在它上面的方法。  
-MainWindow中当点击的时候，相当于事件被触发，然后事件开始调用与它绑定的函数，那么这个函数在哪里呢？——显然就是我们写的这个函数，所以可以说：**在property里面给click事件起的名字其实是把一个函数绑在了这个事件上面去**。而且因为本身要求只允许有一个函数被绑定在一个特定操作上。我们写的这个函数本质上就是只写逻辑，系统帮助我们完成了委托和事件的过程。