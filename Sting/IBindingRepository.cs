using System;

namespace Sting
{
    public interface IBindingRepository
    {
        void Add(IBinding binding);
        IBinding Get(Type type);

        bool Exists(Type type);
    }
}
