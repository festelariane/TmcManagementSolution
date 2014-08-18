using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Tmc.Web.Framework.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
