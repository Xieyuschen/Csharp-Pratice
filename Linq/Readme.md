# 12.4 关于Linq
嗯Linq是很强大的工具，对多种数据源通用（只要事先Inumerable接口的就可以使用Linq）。
功能强大就意味着这一部分不可能很快的就学会以及熟练掌握，即便我上学期学过数据库但是到现在除了对概念有一些认知外其他的是一点不落的还给老师和书本了（捂脸）。也怪我大一第一年太浪，心思没有放在编程上面去。
Linq慢慢练习吧，这会有很多的内容，因为东西很多我要一点点的把它练熟。
嗯就写这么多吧，闲暇写个readme还是有点意思的hhh。

# 12.25 IEnumerator接口以及相关知识的学习。
## Merry Christmas！ Learning carefully and happily! ^_^ :) 

## IEnumerator 以及IEnumerable 的使用：
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

总之，对于一个自己实现的类。在C++里面，想用范围for循环遍历操作就去把迭代器的要求的函数给写一下。而在C#里面想要使用foreach功能就要让这个类继承IEnumerable接口，然后再写一个继承IEnumerator类的辅助类帮助想要进行遍历的类完成一些基本功能。在类里面定义一个返回类型是辅助类型（我觉得打工类更有趣hhh）的GetEnumerator函数（因为这个是接口指定的必须实现的函数）就可以进行简单迭代了。

## IEnumberable<T>的使用：
嗯首先要理解这是个啥玩意？  
——直白来讲，就是这个类型是所有的实现IEnumberale接口的泛型类的统称。比如：  
IEnumerable<string/>