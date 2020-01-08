# 12.21概述  
嗯今天的日子正好是一个回文数，我又开始了web的学习。。之所以用又就是因为我微软这个教学界面已经看了两遍了，我记得两次学的都还不错，但是那又与现在的我有什么关系呢？？该忘记还是会忘记的。。。   
今天开始可能会具体一些，把一些问题和学习体会写在readme里面权当作一个笔记。以后翻看下来还是会有些体会的。加油啦！

# Razor
1. 默认生成界面以及对应的代码位置：  
嗯首先运行起来进入网页后页眉有一个可以选择的栏目，里面有默认名字，Home以及Privacy。那么这几个文件都在哪里存放呢？就存放在`Pages`里面对应的`Index.cshtml`,`Privacy.cshtml`里面去，可以写一些html代码在这个文件夹里面，然后显示在网页上。  
- 小问题1  
**为什么每个cshtml下面都有一个同名的.cs文件？**
2. 对Pages/Movies的文件夹生成基架之后这个文件夹里就多了CRUD的四个cshtml文件以及Index的文件。
3. 初始迁移：
这个环节是干嘛的呢？
- 使用初始迁移
- 使用初始迁移来更新数据库。  
那问题来了，数据库在哪里呢？还没有创建数据库就可以更新嘛？其实这个问题很好回答，初始化数据库就在初始迁移这个地方完成的。因为在这个过程里面我需要输一行命令行代码：
`Add-Migration InitialCreate`，这里的InitialCreate指的就是初始化一个数据库。然后下面还有一行叫`Update-database`.
4. 通过依赖关系注入注册的上下文  
服务在应用程序启动期间通过依赖关系注入进行注册。需要这些服务的组件通过**构造函数**提供相应服务。

# 2020.1.7
# 我又来啦，基础知识争取今天看明白^_^.
## Startup 类
ConfigureServices and Configure are called by the ASP.NET Core runtime when the app starts
The Startup class is specified when the app's host is built. The Startup class is typically specified by calling the WebHostBuilderExtensions.UseStartup<TStartup/> method on the host builder

先明确startup这个类在建立host的时候才被确定，host提供了一些可以被startup构造函数接受的服务。

### Startup中的两个方法` ConfigureServices`和` Configure`.
1. ` ConfigureServices`
Optionally includes a ConfigureServices method to configure the app's services.
- Optional.——如果需要的服务全部可以由构造函数完成就不需要它
- Called by the host before the Configure method to configure the app's services. ——在`Configure()`注册服务之前被host调用添加到管道之中。
- Where configuration options are set by convention.
然后如果想要加上另外的一些服务，就可以调用方法`ConfigureServices`可以再加上去一些。 有些服务类型是不能直接被构造函数所接收的，所以说不能被直接加入的服务通过调用方法`ConfigureServices()`来完成。

- 2. ` Configure`
**The Configure method is used to specify how the app responds to HTTP requests**.
to create the app's request processing pipeline.
在管道里面添加中间件


【问题】：添加服务的构造函数和ConfigureService()和在管道种添加中间件的Configure有什么区别呢？
```
看这两段介绍：
Each middleware component in the request pipeline is responsible for invoking the next component in the pipeline or short-circuiting the chain, if appropriate.

Additional services, such as `IWebHostEnvironment`, `ILoggerFactory`, or anything defined in `ConfigureServices`, can be specified in the `Configure` method signature. These services are injected if they're available.
```
   在管道里的中间件任务是唤起下一个中间件，然后把中间件链打通。ConfigureServices种添加的服务可以被Configure的签名确定，之后如果合适就会被注入。

【问题2】：那么注入服务是注入到了哪里，是注入到了一个合适的中间件里面，还是注入到管道里面去了？
——在DI里面，有这么一句解释：
`Services can be injected into Startup.Configure`
那么，服务可以被注入到`Configure`里面去。
## Dependency Injection
嗯上面这个问题2应该是可以在这里被解决，嘿嘿。
- 什么是依赖？
**A dependency is any object that another object requires.**
——直白一点，就是一个类运行需要另一个类，那么它们就具有依赖关系。
微软的docs里面给了一个直接依赖的例子，比如说类A中使用了类B中的方法，那么A就依赖于B。那么在完成这个类的时候需要在类中直接**将依赖项实例化**，这个模式叫做code dependency。
但是这样有很多问题，如果需要换一个依赖就要重新写类的代码，如果有多个依赖项那么就很难修改。也很难做单元测试。

那么为了解决这个问题，就通过关系依赖注入来解决，方法如下：
1. 使用接口或者基类
2. 在服务容器中注册依赖项，这个服务容器是`IServiceProvider`。通过`Startup.ConfigureServices`方法来注册服务。
把依赖项的实例放到容器中去，而不是直接放到类的里面去。
3. 将服务**注入**到类的构造函数里面去。框架负责创建实例以及在不需要该实例的时候处理掉。

An instance of the service is requested via the constructor of a class where the service is used and assigned to a private field.

【问题3】：在把服务在服务容器注册之后会将服务注入到类中，同时框架也会创建好实例。那么如何**将服务注入到与之对应依赖的类中**去？
——大概是框架自动帮我们匹配了对应的类。



### 中间件
Middleware is software that's assembled into an app pipeline to **handle requests and responses**。
Request delegates are used to build the request pipeline. The request delegates handle each HTTP request.
 An individual request delegate can be specified in-line as an anonymous method (called in-line middleware), or it can be defined in a reusable class.
 **These reusable classes and in-line anonymous methods are middleware**, also called middleware components. 
 Each delegate can perform operations before and after the next delegate. Exception-handling delegates should be called early in the pipeline, so they can catch exceptions that occur in later stages of the pipeline.
 When a delegate doesn't pass a request to the next delegate, it's called short-circuiting the request pipeline. 

 - 中间件顺序
 The order that middleware components are added in the Startup.Configure method defines the order in which the middleware components are invoked on requests and the reverse order for the response. The order is critical for security, performance, and functionality.

 each middleware extension method is exposed on IApplicationBuilder through the Microsoft.AspNetCore.Builder namespace.  


 ## 主机
 ### 通用主机
 A host is an object that encapsulates an app's resources,
 When a host starts, it calls `IHostedService.StartAsync` on each implementation of IHostedService that it finds in the DI container.
 **Question1:**whether the DI container here is the `IServiceBuilder`?

 the host is typically configured, built, and run by code in the Program class. The Main method:
- Calls a CreateHostBuilder method to create and configure a builder object.
- Calls Build and Run methods on the builder object.

这里举一个例子，再理解一下ConfigureService是怎么发挥作用的：
```C#
public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
               services.AddHostedService<Worker>();
            });
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```
函数里面主机设置里面使用了`ConfigureService`这个方法，参数列表处直接使用lambda表达式来完成把单个的服务添加到DI容器里去。