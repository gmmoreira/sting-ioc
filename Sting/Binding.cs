using System;

namespace Sting
{
    public class Binding : IBinding
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        private IServiceFactory ServiceFactory { get; }

        public Binding(Type serviceType, Type implType, IServiceFactory serviceFactory)
        {
            ServiceType = serviceType;
            ImplementationType = implType;
            ServiceFactory = serviceFactory;
        }

        public object Build()
        {
            return ServiceFactory.Build();
        }
    }
}
