# 获取内容的一些分析
网络这一块属于我比较陌生的地方，所以说这里写了一个文档。
# 使用Get和一个网页通信
这里选取了之前一直看的一部小说的页面作为练习。输出就是打开[此界面](http://www.biquge.se/12809/)控制台里面的html代码。  
```csharp

namespace Advance_Csharp
{
   public class Net
    {
        private const string url = "http://www.biquge.se/12809/";
        public static async Task GetDataSample()
        {
            using(var client=new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                    //response的content会把该请求获得的所有页面都抓取出来
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                    WriteLine();
                    WriteLine(responseBodyAsText);
                }
            }
        }
        public static void Main()
        {
            GetDataSample().Wait();
        }
    }
}
```


## 使用正则表达式对文章结果进行筛选
首先解决一个简单的问题，如何对`<dd><a href=\" / 12809 / 35108339.html\">序章缘起</a></dd><dd><a href=\" / 12809 / 35108342.html\">第1章寻仙</a></dd><dd><a href=\" / 12809 / 35108345.html\">第2章拒仙</a></dd>`字符串中的每条信息帅选并打印出来？  
- 使用`.*?`这个操作，没想到吧！  
```csharp
string text="上面那个，有点长就不在这里写了";
Regex r = new Regex(@"<dd>.*?</dd>");
var co= r.Matches(text);
foreach(var item in co)
{
    WriteLine(item.ToString());
}
```

## 使用正则筛选整个的标题
代码还是上面的代码，在下面加了点内容：  
```CSHARP
Regex r = new Regex(@"<dd><a\shref=.*?</dd>");
var co = r.Matches(responseBodyAsText);
foreach (var item in co)
{
    WriteLine(item.ToString());
}
```
输出的结果：
```
<dd><a href="/12809/35108339.html">序章缘起</a></dd>
<dd><a href="/12809/35108342.html">第1章寻仙</a></dd>
<dd><a href="/12809/35108345.html">第2章拒仙</a></dd>
```

## 只想获取章节信息：
要回使用正先行，正后行的正则表达式。replace的方法还不会用。
```csharp
Regex r = new Regex(@"(?<=<dd><a\shref=.*?>)\w\d+.*?(?=<)");
var co = r.Matches(responseBodyAsText);
foreach (var item in co)
{
    WriteLine(item.ToString());
}
```
output:
```
第991章顶天立地
第990章天地初开
第989章完美的脸31/142
第988章开天路三生石
第987章奈何桥孟婆汤
第986章幽皇如梦见轻影
第985章直至如今更不疑
第984章盗花与偷渡3000
第983章所谓伊人在水一方
```                 