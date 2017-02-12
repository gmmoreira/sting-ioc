using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sting
{
    public interface IContainer
    {
        void Register<TService, TImpl>();
        void Register(Type service, Type impl);
        TService Resolve<TService>();
    }
}
