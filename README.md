# Sting - C# IoC container for dependency injection

## Usage

### Creating a new container
```csharp
using Sting;

var container = new Container();
```

### Registering a service
```csharp
container.Register<IPublisher, MyPublisher>();
```

### Resolving a service
```csharp
var publisher = container.Resolve<IPublisher>();
publisher.Publish("Hello");
```

### Singleton service
```csharp
container.RegisterSingleton<IPublisher, MyPublisher>();

var publisher1 = container.Resolve<IPublisher>();
var publisher2 = container.Resolve<IPublisher>();

Assert.True(publisher1 == publisher2);
```

## Code Status

![Travis CI build status](https://travis-ci.org/gmmoreira/sting-ioc.svg?branch=master)
