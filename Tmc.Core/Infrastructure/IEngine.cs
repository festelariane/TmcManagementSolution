using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Configuration;

namespace Tmc.Core.Infrastructure
{
    public interface IEngine
    {
        void Initialize(TmcConfig config);
        T Resolve<T>() where T : class;
    }
}
