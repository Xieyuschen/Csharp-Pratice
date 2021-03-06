# 12.2 委托和事件的练习
----------------
- **前言**
&emsp;
嗯之前突击了一下C#就去看一些应用设计和网页制作，确实有所收获但其实最近在看ASP.NET Core中有很多概念本来我就不熟悉，再加上C#的语法比较不熟练导致我基本上不能再往前走下去了。这时候回来复习一下语法再写一点小控制台程序来熟悉语法还是蛮有意思的。
- **总览**
&emsp;
本例目前一共包含了5个.cs程序，这五个程序的发展顺序为：
继承接口类的使用方法（包括踩的坑）——>在原有基础上实现委托——>定义事件并实现——>将对事件订阅的过程封装在类中而非Main函数中

- 一些注意事项
&emsp;
嗯这次很多注意事项在.cs 文件中都有标注，就不在这里展示了。


# 12.3 更新：如何组织delegate和event
之前写了一下如何实现delegate和event，但是之前写的时候还是借鉴了教科书的书写同时没有参数就导致了并不很透彻的理解传递的过程。所以今天通过6和7两个已经实现的实例来解决以上问题。

# delegate：
&emsp;
- delegate的一些基本注意事项
1. delegate的声明要放在和要委托类平行的位置。如我想delegate student类的AddScore()方法。那么需要把delegate声明在class student 的上边。即必须和student类是平行关系而不能包含在类中去。
2. delegate其实会隐式生成一个类，所以在实例化累的时候可以在构造函数里面直接绑定某些类的成员函数，可以绑定若干个类的成员函数。部分示例代码如下：
```C#
        class Relative 
        class try1{
            public void Show()
            {
                Console.WriteLine("delegate different class is right");
            }
        }
        static void Main(string[] arg)
        {
            Relative relative = new Relative();
            try1 try1 = new try1();
            SayToYou sayToYou = new SayToYou(relative.Hello);
            sayToYou += try1.Show;           
            sayToYou();

        }
    }
}
```
3. 即通过delegate对象可以类似于把若干类中的若干签名相同的成员函数放在一个新的容器中，一旦我对这个容器输入合适的值并调用，即调用了所委托的所有方法（若干个[classname].[funcname]被调用，如果一个个的写及要写对象又要写方法而使用委托在创建实例进行绑定之后就不需要在使用类对象进行调用了，delegate生成的隐式类会自动帮助我们调用这些对象）。

以上的第三条其实点明了delegate起到的作用，再重复一遍：
**使用委托可以一次性调用若干个来自不同类中签名相同的成员函数，在完成绑定之后就可以通过委托类的对象对这些函数进行调用**。
如委托对象为： sayToyou，那么我要调用绑定在委托对象sayToyou上的函数只需要sayToYou(//合适的参数列表)即可。


# Event
- 如何使用事件这个东西？
&emsp;
首先事件这个东西是干嘛的呢？——在某件事情发生的时候我要做一些什么事情，这么一个整体就是一个事件。那么显然事件的定义需要解决两个问题：
1. 如何判断特定的事情发生了？
2. 在成功判断特定的事情发生了，如何完成相应的应对措施（即发生时我要做些什么）。

在解决问题之前，还需要了解一些event使用的特点：
- 注意事项：
1. 首先如果想要使用事件必须要创建一个类，我们可以把这个类叫做事件类（因为事件是在这个类中声明的）。那么提到事件我们就难免不想到委托，因为委托实在是太方便了。所以如果我需要使用委托的话还是需要在类外定义委托（和事件类平行）。
2. 现在首先讨论使用委托，事件类的定义方法如下：可以看出有一个比较难以理解的地方就是Show()函数里面又调用了事件Reminder。这个是什么意思呢？
这时候就需要去看关于事件的定义了，事件定义的前面有一个委托Attention。那说明Reminder是要接受Attention这个委托，那么既然委托接受一个参数，这里面的方法都需要接受一个参数。
```C#
public delegate void Attention(int x);

public class Operation
        {
            public event Attention Reminder;
            public void Show(int x)
            {
                Reminder(x);
            }

        }
```
3. 如何使用事件？
&emsp;
首先实例化事件类的一个对象，然后可以对这个对象来添加委托或者一些其他类的成员函数。但是如果想要运行的、这个事件的话就必须使用事件类中的方法（有点废话但是在这里比较复杂看迷糊之后就很容易出现这种脑残问题，仅限于个人hhh）。这也是为什么在成员函数里面有对这个事件的调用，参数通过： 事件——>绑定在事件上的委托——>绑定在委托上的各个函数  这条途径进行传递。~~现在我还不能确定是否可以不使用委托直接进行绑定~~。在下面可以去试一下。
————好我一定确定我是个憨憨了，声明事件必须使用委托，没有委托根本无法定义事件。

好那么可以来开始解决最上面的两个问题了。
1. 如何判断特定的事件发生了？——当事件类实例化的对象调用成员函数的时候就可以判断事件发生了。
2. 如何完成应对措施？———成员函数中会调用委托，传递给事件的参数会传递给委托然后在被执行。

嗯，今天写的东西就这么多了。

# 12.4 的更新：
-泛型委托与泛型事件：
&emsp;
泛型在委托和事件中的应用并没有什么特别需要注意的，只是核心的内容需要在设计的时候完成。如果提前订阅好而不在main函数中使用，可以考虑在构造函数中完成。
在之前有所体现。

# Action与Func的泛型委托。
- Action如何使用？
Action是提前定义好的泛型，最多只接受16个参数。只接受返回类型为void类型函数的委托。
- Func 如何使用？
Func是定义好的泛型委托，只能接受有返回类型函数的委托。
-Func 和 Action的区别是什么？
Func必须接受有返回值的函数，并把接受委托的函数返回值类型写在Func泛型列表的最后面。即尖括号列表内类型顺序为：参数列表的类型顺序，最后是返回类型。而返回类型为void的函数若想要委托就必须使用Action。举出一些示例如下，省略函数的实现代码。
```C# 
int Calculate(double x,double y){};
Func<double,double,int> func=new Func<double,double,int>();
double div(int x,int y){};
Func<int,int,double> func2=new Func<int,int,double>();
string Show(int a,double b); 
Func<int,double,string> func3=new Func<int,double,string>();
//Func不能接受无返回值的函数
void Print(int){};
//错误，因为Func尖括号列表中最后一个类型不能是void。
//Func<int,void> func4=new Func<int,void>();
//在这里要使用Action进行委托
Action<int> action=new Action<int>();

```

