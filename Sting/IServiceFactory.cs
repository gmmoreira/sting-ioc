using System;

namespace Sting
{
    public interface IServiceFactory
    {
        object Build();
    }
}