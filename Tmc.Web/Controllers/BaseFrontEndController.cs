using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tmc.Core.Infrastructure;
using Tmc.Web.Framework.Controllers;

namespace Tmc.Web.Controllers
{
    public abstract class BaseFrontEndController : BaseController
    {
        protected virtual ActionResult InvokeHttp404()
        {
            IController errorController = EngineContext.Current.Resolve<CommonController>();
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
    }
}