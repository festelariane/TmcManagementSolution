using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Configuration;
using Tmc.Core.DependencyManagement;

namespace Tmc.Core.Infrastructure
{
    public class TmcEngine : IEngine
    {
        private ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get
            {
                return _containerManager;
            }
        }
        public TmcEngine()
        {
            //var config = ConfigurationManager.GetSection("TmcConfig") as TmcConfig;
            var config = new TmcConfig();
            var builder = new ContainerBuilder();
            var container = builder.Build();
            _containerManager = new ContainerManager(container);
            _containerManager.AddComponentInstance<TmcConfig>(config, "tmc.configuration");
            _containerManager.AddComponentInstance<IEngine>(this, "tmc.engine");
            _containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("tmc.typeFinder");

            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            _containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                {
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                }
                drInstances = drInstances.AsQueryable().OrderBy(dri => dri.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                {
                    dependencyRegistrar.Register(x,typeFinder);
                }
            });
        }
        public void Initialize(TmcConfig config)
        {
            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                //RunStartupTasks();
            }
        }
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
    }
}
