using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac;

namespace Tmc.Core.DependencyManagement
{
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        public static readonly object HttpRequestTag = "AutofacWebRequest";
        public void Init(HttpApplication context)
        {
            context.EndRequest += context_EndRequest;
        }

        static ILifetimeScope LifetimeScope
        {
            get
            {
                return (ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)];
            }
            set
            {
                HttpContext.Current.Items[typeof(ILifetimeScope)] = value;
            }
        }
        void context_EndRequest(object sender, EventArgs e)
        {
            ILifetimeScope lifetimeScope = LifetimeScope;
            if (lifetimeScope != null)
            {
                lifetimeScope.Dispose();
            }
        }

        public void Dispose()
        {
            
        }

        public static ILifetimeScope GetLifetimeScope(ILifetimeScope container,
            Action<ContainerBuilder> configurationAction)
        {
            if (HttpContext.Current != null)
            {
                return LifetimeScope ?? (LifetimeScope = InitializeLifetimeScope(configurationAction, container));
            }
            else
            {
                return InitializeLifetimeScope(configurationAction, container);
            }
        }

        static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction,
            ILifetimeScope container)
        {
            if (configurationAction == null)
            {
                return container.BeginLifetimeScope(HttpRequestTag);
            }
            return container.BeginLifetimeScope(HttpRequestTag, configurationAction);
        }
    }
}
