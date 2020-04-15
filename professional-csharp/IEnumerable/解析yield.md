# yield的使用：
yield的使用主要是用来使用`foreach`进行遍历的，一个继承接口`IEnumerable`的类只要实现了`GetEnumerator()`方法就可以使用foreach进行遍历。  

## 什么是`IEnumerator`?
IEnumerator我们把它叫做**枚举器**。他是干什么用的呢？简单来说就是用来保存当前遍历的一个元素，通过这个枚举器的不断移动来完成遍历，类似于C++中的迭代器。  
>在C++的迭代器中需要对迭代器类型定义运算符`++`，以使程序知道在遍历的时候如何移动迭代器。

那么在C#中，这里的枚举器也类似，其中有一个方法`MoveNext()`，同样定义了枚举器是如何移动的，当然使用yield是不需要定义这个函数的。具体原因请看下面叙述。

## yield的作用：
使用yield的地方我们叫做迭代块，编译器会生成一个yield类型，里面包含着一个状态机。  
可以把yield类型看作是内部类Enumerator，外部类的方法`GetEnumerator()`实例化并**返回一个yield类型**。在yield类型中会自动进行`MoveNext()`的逻辑处理。  
### Demo
举一个例子，是将C#高级编程中的一些实例代码进行修改所得：  
那么可以看出，进行了轮换的输出，也即MoveNext被自动的生成，yield的使用会使编译器生成一个类，其中列出了这个枚举路线上的所有值。如本例从circle开始，对于便利的类型为：  
`circle->cross->circle->cross->circle->...`无穷无尽，一直生成着。在本例处因为有一个私有成员`maxMoves`的限制之后进行了`yield breal`(即遍历结束)。如果不进行限制的话就会无限输出遍历永无尽头。
```csharp
using System;
using System.Collections;
using System.Text;

namespace Advance_Csharp
{
    class Class1
    {
        public class Game
        {
            private IEnumerator _circle;
            private IEnumerator _cross;
            public Game()
            {
                _circle = Circle();
                _cross =Cross();
            }
            private int _move;
            const int maxMoves = 8;

            public IEnumerator Cross()
            {
                while (true)
                {
                    Console.WriteLine($"Cross,move {_move};");
                    _move++;
                    if(_move > maxMoves)
                    {
                        yield break;
                    }
                    yield return _circle;
                }
            }
            public IEnumerator Circle()
            {
                while (true)
                {
                    Console.WriteLine($"Circle,move {_move};");
                    _move++;
                    if (_move > maxMoves)
                    {
                        yield break;
                    }
                    yield return _cross;
                }
            }

        }
        public static void Main()
        {
            Game game = new Game();
            //像这种game.Cross()并没有进入函数
            IEnumerator enumerator = game.Cross();
            //MoveNext是如何定义的？
            while (enumerator.MoveNext())
            {
                enumerator = enumerator.Current as IEnumerator;
            }
        }
    }
}

```
此程序的输出结果如下：  
```
Cross,move 0;
Circle,move 1;
Cross,move 2;
Circle,move 3;
Cross,move 4;
Circle,move 5;
Cross,move 6;
Circle,move 7;
Cross,move 8;
```

### 永无尽头遍历的修改（接上例）
```csharp
//只将修改处的代码展示出来。
 public IEnumerator Cross()
{
    while (true)
    {
        Console.WriteLine($"Cross,move {_move};");
        _move++;
        yield return _circle;
    }
}
public IEnumerator Circle()
{
    while (true)
    {
        Console.WriteLine($"Circle,move {_move};");
        _move++;
        yield return _cross;
    }
}
```
输出的结果（随便节选的一段）：  
```csharp
//这种输出是永无止境的，因为没有yield break
//MoveNext永远不会停下来
Cross,move 81534;
Circle,move 81535;
Cross,move 81536;
Circle,move 81537;
Cross,move 81538;
Circle,move 81539;
Cross,move 81540;
Circle,move 81541;
Cross,move 81542;
Circle,move 81543;
Cross,move 81544;
Circle,move 81545;
Cross,move 81546;
Circle,move 81547;
Cross,move 81548;
Circle,move 81549;
Cross,move 81550;
Circle,move 81551;
Cross,move 81552;
Circle,move 81553;
Cross,move 81554;
```