using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Core.Common;
using Tmc.Web.Models.Common;
using Tmc.Core.Domain.Extensions;

namespace Tmc.Web.Controllers
{
    public class CommonController : BaseFrontEndController
    {
        private readonly IWorkContext _workContext;
        public CommonController(IWorkContext workContext)
        {
            this._workContext = workContext;
        }
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }
        [ChildActionOnly]
        public ActionResult HeaderLinks()
        {
            var customer = _workContext.CurrentCustomer;
            var model = new HeaderLinksModel();
            if(customer != null)
            {
                model.IsAuthenticated = customer.IsAdmin() || customer.IsRegisteredUser();
                model.CustomerName = customer.UserName;
                model.CustomerId = customer.Id;
                model.IsAdmin = customer.IsAdmin();
            }
            return PartialView(model);
        }
    }
}