using System;

namespace Sting
{
    public interface IBinding
    {
        Type ServiceType { get; }
        Type ImplementationType { get; }

        object Build();
    }
}