# Sting - C# IoC container for dependency injection

## Usage

### Creating a new container
```csharp
using Sting;

var container = new Container();
```

### Registering a service
```csharp
interface ILog { void Info(string text); }
class Log : Ilog 
{ 
    public void Info(string text) 
    { 
        Console.WriteLine(text);
    }
}
container.Register<ILog, Log>();
```

### Resolving a service
```csharp
var logger = container.Resolve<Log>();
logger.Info("Hello");
```

### Singleton service
```csharp
container.RegisterSingleton<ILog, Log>();

var logger1 = container.Resolve<ILog>();
var logger2 = container.Resolve<ILog>();

Assert.True(logger1 == logger2);
```

### Resolve dependencies
```csharp
interface IPublisher { void Publish(string message); }
class MyPublisher : IPublisher 
{
    private ILog Log { get; }

    public MyPublisher(ILog log) 
    { 
        Log = log; 
    }

    public void Publish(string message) 
    {
        Log.Info($"Publishing message: {message}");
        // publish...
    }
}

container.Register<ILog, Log>();
container.Register<IPublisher, MyPublisher>();

var publisher = container.Resolve<IPublisher>();
publisher.Publish("Hello");
```

## Code Status

![Travis CI build status](https://travis-ci.org/gmmoreira/sting-ioc.svg?branch=master)
