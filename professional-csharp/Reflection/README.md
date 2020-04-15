# 动态类型不会进行类型检查
```csharp
Console.WriteLine($"{employee.FirstName} {employee.LastName} lives in {employee.City}, {employee.State}.");
```
比如说这一句，如果.后面的名字拼错了也不会直接出红线，而是在运行的时候给出错误

## 一个小问题：
此程序中，每个对象作为一个IDictionary集合，为什么在访问的时候可以使用`.`运算符进行访问成员呢？  
&emsp;&emsp;提出一个想法，因为是动态类型所以可以随意转换，当加数据的时候作为dictionary，之后作为对象。 
&emsp;&emsp;这个叫做`expando dictionary`,在弱类型中
```csharp
dynamic d = new ExpandoObject();
//这里键值对string-object，string是因为属性名，必须为string
//对于值来说，不知道是什么类型的，那就直接使用object，而且object是不能够具体为别的类型的
//比如说<string,stirng>就会报错，因为这个ExpandoObject类是不能转换成IDictionart<string,string>的
//如果想要进行强制类型转换，必须要等把值取出来再转换，如：(string)d.key;
((IDictionary<string, object>)d).Add("hello","world");
Console.WriteLine(d.hello);
```

