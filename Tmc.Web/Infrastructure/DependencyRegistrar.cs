using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Core.Common;
using Tmc.Core.DependencyManagement;
using Tmc.Core.Infrastructure;
using Tmc.Web.Framework;

namespace Tmc.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerRequest();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}