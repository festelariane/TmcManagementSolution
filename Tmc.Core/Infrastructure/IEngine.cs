using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Configuration;
using Tmc.Core.DependencyManagement;

namespace Tmc.Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }
        void Initialize(TmcConfig config);
        T Resolve<T>() where T : class;

        object Resolve(Type type);
    }
}
