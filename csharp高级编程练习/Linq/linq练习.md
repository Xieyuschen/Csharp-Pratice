# Linq的练习
在C#高级编程里面这是一整个Demo，那么就可以把这些Demo整理成练习题然后做一遍体会一下^_^.

## 1. 查询所有来自巴西的世界冠军：
```csharp
//linq语句必须要以select语句结尾
var result = from r in Formula1.GetChampions()
                where r.Country == "Brazil"
                orderby r.Wins descending
                select r;
```
## 2. 找出赢得至少15场比赛的巴西和奥地利赛车手Austria
```csharp
var result = from r in Formula1.GetChampions()
                where (r.Country == "Brazil" || r.Country=="Austria")&&r.Wins>=15
                orderby r.Wins descending
                select r;
```
## 3. 返回姓氏以A开头的选手
```cpp
var result = from r in Formula1.GetChampions()
                .Where((r, index) => r.FirstName.StartsWith("A") && index % 2 == 0)
                select r;
```

## 4. 筛选驾驶法拉利Farrari夺冠的所有冠军
```csharp
var result = from r in Formula1.GetChampions()
                from r1 in r.Cars
                where r1 == "Ferrari"
                orderby r.FirstName
                select r;
```
## 5. 按照国家分组，并列出冠军数大于2的国家
```csharp
//常规表示方法
var result = from r in Formula1.GetChampions()
                group r by r.Country into g
                orderby  g.Count() descending,g.Key
                where g.Count()>1
                select new
                {
                    Country=g.Key,
                    count=g.Count()
                };

foreach(var item in result)
{
    Console.WriteLine($"{item.Country,-10} ,{item.count} ");
}

//使用let语句创建变量以减少重复调用函数
var result = from r in Formula1.GetChampions()
                group r by r.Country into g
                let count=g.Count()
                orderby count descending,g.Key
                where count>1
                select new
                {
                    Country=g.Key,
                    count=g.Count()
                };
```

## 6. 查询获得冠军数大于二的国家，并打印出获得冠军的所有选手姓名。
无论查到了什么东西，最后结果类型一定是在select语句里

```csharp

var result = from r in Formula1.GetChampions()
                group r by r.Country into g
                let count = g.Count()
                orderby count descending, g.Key
                where count > 1
                select new
                {
                    Country = g.Key,
                    count = g.Count(),
                    Racers = from item in g
                            where item.Country == g.Key
                            select item.FirstName + " " + item.LastName
                };

foreach(var item in result)
{
    Console.WriteLine($"{item.Country,-10} ,{item.count} ");
    foreach(var p in item.Racers)
    {
        Console.Write(p+"  ");
    }
    Console.WriteLine();
}
```

## 7. 按年份查找每年的冠军选手，按照获奖年份从古到今进行排列，打印出选手的获奖年份以及姓名。

```cpp
var result = from r in Formula1.GetChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name= r.ToString(),
                    Time = y
                };

foreach(var item in result)
{
    Console.WriteLine($"{item.Name} {item.Time}");
}
```
## 8. 查询每年的车队冠军：
```cpp
var result = from r in Formula1.GetConstructorChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name= r.Name,
                    Time = y
                };
```
## 9. 内连接查询--获得一个年份列表，列出每年的赛车手冠军和车队冠军
在使用join的内连接查询时，这里连接的键是不能够重复的。
```csharp
var racers = from r in Formula1.GetChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name = r.ToString(),
                    Time = y
                };
var teams = from r in Formula1.GetConstructorChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name= r.Name,
                    Time = y
                };
var result = from r in racers
                join t in teams on r.Time equals t.Time
                select new
                {
                    RacerName = r.Name,
                    TeamName = t.Name,
                    Time = r.Time
                };
foreach(var item in result)
{
    Console.WriteLine($"\"{item.RacerName}\" in team \"{item.TeamName}\" won in {item.Time}");
}
```

### 输出结果：
```
"Mike Hawthorn" in team "Vanwall" won in 1958
"Jack Brabham" in team "Cooper" won in 1959
"Jack Brabham" in team "Cooper" won in 1960

//many pieces here
//................

"Lewis Hamilton" in team "Mercedes" won in 2014
"Lewis Hamilton" in team "Mercedes" won in 2015
"Nico Rosberg" in team "Mercedes" won in 2016
```

## 10. 左外连接查询 
由第九问的解答可以发现，第一条查询记录是从1958开始的，而赛车手的冠军从1950年就开始了，那么如果我想把这部分也查出来怎么办？那么就使用左连接。  
- 左连接使用`join`子句和`DefaultEmpty()`方法定义。  
```csharp
var racers = from r in Formula1.GetChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name = r.ToString(),
                    Time = y
                };
var teams = from r in Formula1.GetConstructorChampions()
                from y in r.Years
                orderby y
                select new
                {
                    Name= r.Name,
                    Time = y
                };
var result = from r in racers
                join t in teams on r.Time equals t.Time into rt
                //这一句就是如果t没有找到符合条件的时候那么就调用DefaultIfEmpty()函数。
                from t in rt.DefaultIfEmpty()
                select new
                {
                    RacerName = r.Name,
                    TeamName = t?.Name,
                    Time = r.Time
                };
foreach(var item in result)
{
    Console.WriteLine($"\"{item.RacerName}\" in team \"{item?.TeamName}\" won in {item.Time}");
}
```

## 组查询--根据历届比赛前三名的数据，统计获得过第一名选手的获奖情况
这个查询比较生僻，之后再找几个来练习。
```csharp
var racers = Formula1.GetChampionships()
    .SelectMany(cs => new List<RacerInfo>()
    {
        new RacerInfo
        {
            Year=cs.Year,
            Position=1,
            Name=cs.First
        },
        new RacerInfo
        {
            Year=cs.Year,
            Position=2,
            Name=cs.Second
        },
        new RacerInfo
        {
            Year=cs.Year,
            Position=3,
            Name=cs.Third
        }
    });

var q = from r2 in Formula1.GetChampions() 
        join r in racers on
        new
        {
            Name = r2.FirstName + " " + r2.LastName
        }
        equals
        new
        {
            Name = r.Name
        }
        into yearResult
        select new
        {
            Name = r2.FirstName + " " + r2.LastName,
            Wins = r2.Wins,
            Starts=r2.Starts,
            Result=yearResult
        };
    
foreach(var item in q)
{
    Console.WriteLine(item.Name);
    foreach(var year in item.Result)
    {
        Console.WriteLine($"{year.Year} {year.Position}");
    }
}
```