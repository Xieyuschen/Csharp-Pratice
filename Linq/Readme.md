# 12.4 关于Linq
嗯Linq是很强大的工具，对多种数据源通用（只要事先Inumerable接口的就可以使用Linq）。
功能强大就意味着这一部分不可能很快的就学会以及熟练掌握，即便我上学期学过数据库但是到现在除了对概念有一些认知外其他的是一点不落的还给老师和书本了（捂脸）。也怪我大一第一年太浪，心思没有放在编程上面去。
Linq慢慢练习吧，这会有很多的内容，因为东西很多我要一点点的把它练熟。
嗯就写这么多吧，闲暇写个readme还是有点意思的hhh。
# 练习代码的一些说明:
这个部分是2.4专门加上去的,把每个练习的目的写清楚,方便以后翻看或者别人看(应该不会有人看hh).  
- **9&&10**  
9练习了继承接口`IEnumerable`的类要如何写,10 练习了继承`INumerable<T>`的类要如何写.
# 12.25 IEnumerator接口以及相关知识的学习。
## Merry Christmas！ Learn carefully and happily! ^_^ :) 

# IEnumerator 以及IEnumerable 的使用：
- IEnumerator是所有非泛型枚举类型接口。在foreach语句中，**隐藏了枚举数的复杂性**。
- 枚举器可以用来**读取集合中的数据**，不能用于修改基础集合。
1. 最初，枚举数定位在集合中的第一个元素前面。在读取current值Move Next之前，必须调用方法以将枚举器前进到元素的第一个元素。
2. 在调用 Current或MoveNext 之前，Reset返回同一对象。MoveNext讲Current设置为下一个元素。如果MoveNext越过集合末尾，那么枚举器将定位在集合中的**最后一个元素之后**，并MoveNext返回false。当枚举器位于此位置时，对MoveNext的后续调用也将返回。

- 嗯所以根据调用实例来对IEnumerator有一个更加深入的了解：
使用调试功能进行跟踪查看——首先在foreach这里：
`foreach (Person p in peopleList)`
1. 在进入foreach循环之后首先被使用的是peopleList（类型为People类），此时进入People类中的`PeopleEnum GetEnumerator()`方法中去。这个方法返回一个新创建的类PeopleEnum。
嗯这里要怎么理解呢？这里并不是直接对People这个类来进行操作而是创建了一个新类对新类中的相同内容进行操作。这就是foreach对自己实现的类是不能够进行修改操作的（大概支持遍历操作的数据类型也都不支持修改的原理和此处相同）。
也就是说我加上接口IEnumerable说明这个类支持迭代器操作和遍历foreach操作。类似于C++中的迭代器操作，一个自定义类型如果想要使用迭代器类型那么就必须有begin()和end()函数。、
2. 在完成动态分配一块新内存之后然后调用构造函数后返回完成了peopleList的读取。然后再foreach的小括号内继续调试：  
然后调试进入in上，再调试时开始对p赋一个值，继续调试系统操作的peopleEnum类中的MoveNext函数，然后对列表中索引为position的元素赋给p，然后处理打括号内的逻辑业务。当MoveNext函数返回false的时候，foreach就退出了。

&emsp;&emsp;总之，对于一个自己实现的类。在C++里面，想用范围for循环遍历操作就去把迭代器的要求的函数给写一下。而在C#里面想要使用foreach功能就要让这个类继承IEnumerable接口，然后再写一个继承IEnumerator类的辅助类帮助想要进行遍历的类完成一些基本功能。在类里面定义一个返回类型是辅助类型（我觉得打工类更有趣hhh）的GetEnumerator函数（因为这个是接口指定的必须实现的函数）就可以进行简单迭代了。  



# 实现一个继承IEnumerable类的细节与经验总结：
1. &emsp;&emsp;一个类继承IEnumerable接口的目的是这个类可以完成迭代工作，即这个类一般是其他内容的集合，存储了一系列同质内容，支持使用`foreach`进行遍历操作。  
2. &emsp;&emsp;在C++中前一阵子学习STL的时候也实现了链表的可迭代，链表里的迭代器`iterator`和这里的这个接口是相同的.但在C++中封装的并没有C#这里这么好,全部都是裸露的,需要使用指针进行操作.这里封装的更好,使用接口的方式有效的减少了问题的出现.
3. &emsp;&emsp;在写的时候,完成一个可迭代的类必须要有三部分:  
    >- 一个基础的类,未来用来作为一个可迭代类的组成元素.比如说一个班级作为一个类,那么下面的基础元素就是学生类.
    >- 一个继承`IEnumerable`的类,这个是用来使用的主体.
    >- 一个辅助的类,继承`IEnumerator`接口,来完成`IEnumerable`接口中要求的`IEnumerator IEnumerable.GetEnumerator()`方法,这个方法要求返回一个`IEnumerator`的对象, 自然需要写一个继承`IEnumerator`接口的对象.

    按照两个接口的提示,把各个接口都实现以下就完事了.嗯其它的就不多说了.



# `IEnumberable<T>`

## `IEnumberable<T>`
嗯首先要理解这是个啥玩意？  
——直白来讲，就是这个类型是所有的实现IEnumberale接口的泛型类的统称。比如：  
`IEnumerable<string>`这个类型包含了若干个实现迭代功能的类型，比如`List<string>`,`string[] array`这一类。`IEnumerable<string>`有更好的表示特性。嗯很棒！  
再给出一个更简洁的定义：**公开枚举数，该枚举数支持在指定类型的集合上进行简单迭代。**
也就是说IEnumberable是在object上进行迭代，而带上泛型参数T之后就制定了在特定集合上进行迭代。
第7，8个代码是直接把微软给的例子给抄来了。嗯加了点注释hhh


公开枚举数，该枚举数支持在**指定类型的集合**上进行简单迭代。
- 要了解这个接口,首先关注接口`IEnumerator<T>`:  
## `IEnumerator<T>`
支持在泛型集合上进行简单迭代。
```csharp
public interface IEnumerator<out T> : IDisposable, System.Collections.IEnumerator
```
嗯可见此接口继承了接口`IDisposable`和`Collection.IEnumerator`,那么在实现这个类的时候就必须实现这两个接口.

### 注解:
`IEnumerator<T>` 是所有泛型枚举器的基接口。枚举器可用于读取集合中的数据，但不能用于修改基础集合。   
&emsp;&emsp;实现此接口需要实现非泛型 `IEnumerator` 接口。 `MoveNext()` 和 `Reset()` 方法不依赖于 T，并且仅出现在非泛型接口上。`Current` 属性显示在两个接口上，并且具有不同的返回类型。 实现非泛型 `Current` 属性作为显式接口实现。 这允许非泛型接口的任何使用者使用泛型接口。  
&emsp;&emsp;此外，`IEnumerator<T>` 实现 IDisposable，这要求您实现 Dispose() 方法。 这使您可以在使用其他资源时关闭数据库连接或释放文件句柄或类似的操作。 如果没有要释放的其他资源，请提供空的 Dispose() 实现。
- 嗯那么在实现`IEnumerable<T>`接口里面,同样需要实现泛型和非泛型的接口.

## 如何理解泛型?  
首先来说泛型呢,就是可以类型在实例化的时候才被确定.可以是多种多样的类型,如内置类型(int,double等等)或者自定义的类型.
截自`StackOverflow`中的一个[优质回答](https://stackoverflow.com/questions/4972170/difference-between-ienumerable-and-ienumerablet).
>The reason why we need IEnumerable<T> is so that we can iterate in a type-safe way, and propagate that information around. If I return an IEnumerable<string> to you, you know that you can safely assume everything returned from it will be a string reference or null. With IEnumerable, we had to effectively cast (often implicitly in a foreach statement) each element that was returned from the sequence, because the Current property of IEnumerator is just of type object. As for why we still need IEnumerable - because old interfaces never go away, basically. There's too much existing code using it.

简而言之,就是更加的安全.