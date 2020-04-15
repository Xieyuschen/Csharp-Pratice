# 异步
如何理解异步呢？首先强调一点，**异步和线程关系不大**，不要看到异步就想起多线程的问题，他们中间是有点差别的。  
## MSDOCS里面的刷盘子
[点这里](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/)  
这个教程中，很好地说明了什么是异步，什么是多线程。  
&emsp;&emsp;在做早餐的时候，烧水的时候去烤面包就是异步。这是一个真异步，因为烧水不是我（这里我可以理解为一个线程，而因为早餐是我一个人做的，所以是单线程）用用爱烧水的，而是用茶壶烧水的。这里的茶壶可以认为是一个应用程序，如OS的IO或者对数据库的查询。在这些事情做的时候，可以使用异步。就好比烧水的时候我不需要时刻盯着，我可以去做别的事情。查询数据库也可以使用异步进行，在这个过程中可以进行一些其他的操作。