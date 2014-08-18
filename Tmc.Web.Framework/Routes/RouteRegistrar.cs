using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Tmc.Core.Infrastructure;

namespace Tmc.Web.Framework.Routes
{
    public class RouteRegistrar : IRouteRegistrar
    {
        protected readonly ITypeFinder typeFinder;
        public RouteRegistrar(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        public virtual void RegisterRoutes(RouteCollection routes)
        {

        }
    }
}
