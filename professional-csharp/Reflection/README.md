# 动态类型不会进行类型检查
```csharp
Console.WriteLine($"{employee.FirstName} {employee.LastName} lives in {employee.City}, {employee.State}.");
```
比如说这一句，如果.后面的名字拼错了也不会直接出红线，而是在运行的时候给出错误

## 一个小问题：
此程序中，每个对象作为一个IDictionary集合，为什么在访问的时候可以使用`.`运算符进行访问成员呢？  
&emsp;&emsp;提出一个想法，因为是动态类型所以可以随意转换，当加数据的时候作为dictionary，之后作为对象。  
```csharp
dynamic d = new ExpandoObject();
((IDictionary<string, object>)d).Add("hello","world");
Console.WriteLine(d.hello);
```

