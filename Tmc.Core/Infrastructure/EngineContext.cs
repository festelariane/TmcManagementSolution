using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Configuration;

namespace Tmc.Core.Infrastructure
{
    public class EngineContext
    {
        public static IEngine Current
        {
            get
            {
                if(Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if(Singleton<IEngine>.Instance == null || forceRecreate)
            {
                //var config = ConfigurationManager.GetSection("TmcConfig") as TmcConfig;
                var config = new TmcConfig();
                var currentEngine = CreateEngineInstance(config);
                currentEngine.Initialize(config);
                Singleton<IEngine>.Instance = currentEngine;
            }
            return Singleton<IEngine>.Instance;
        }

        private static IEngine CreateEngineInstance(TmcConfig config)
        {
            if (config != null && !string.IsNullOrEmpty(config.EngineType))
            {
                var engineType = Type.GetType(config.EngineType);

                if (engineType == null)
                    throw new ConfigurationErrorsException("The type '" + engineType + "' could not be found. Please check the configuration file.s");
                if (!typeof(IEngine).IsAssignableFrom(engineType))
                    throw new ConfigurationErrorsException("The type '" + engineType + "' doesn't implement 'Tmc.Core.Infrastructure.IEngine' and cannot be configured in configuration file for that purpose.");
                return Activator.CreateInstance(engineType) as IEngine;
            }
            return new TmcEngine();
        }
    }
}
