using System;
using System.Collections.Generic;

namespace Sting
{
    public class DictionaryBindingRepository : IBindingRepository
    {
        private IDictionary<Type, IBinding> Repo { get; }

        public DictionaryBindingRepository() : this(new Dictionary<Type, IBinding>())
        {
        }

        public DictionaryBindingRepository(IDictionary<Type, IBinding> dict)
        {
            Repo = dict;
        }

        public void Add(IBinding binding)
        {
            Repo.Add(binding.ServiceType, binding);
        }

        public bool Exists(Type type)
        {
            return Repo.ContainsKey(type);
        }

        public IBinding Get(Type type)
        {
            return Repo[type];
        }
    }
}