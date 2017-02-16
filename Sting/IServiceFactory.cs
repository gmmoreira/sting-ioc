using System;

namespace Sting
{
    public interface IServiceFactory
    {
        IConstructorResolver ConstructorResolver { get; }
        object Build();
    }
}