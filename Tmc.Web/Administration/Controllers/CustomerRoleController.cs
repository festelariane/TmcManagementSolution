using System.Collections.Generic;
using System.Web.Mvc;
using Tmc.Admin.Models.CardTypes;
using Tmc.BLL.Contract.Cards;
using Tmc.Core.Domain.Cards;
using Tmc.Web.Framework.KendoUi;
using Tmc.Admin.Extensions;
using System.Linq;
using Tmc.Web.Framework.Common;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Domain.Customers;
using Tmc.Web.Framework.FilterAttributes;

namespace Tmc.Admin.Controllers
{
    [AdminAuthorize]
    public class CustomerRoleController : BaseAdminController
    {
        private readonly ICustomerBiz _customerBiz;

        public CustomerRoleController(ICustomerBiz customerBiz)
        {
            this._customerBiz = customerBiz;
        }
      
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var roles = _customerBiz.GetAllCustomerRoles();
            var gridModel = new DataSourceResult
            {
                Data = roles,
                Total = roles.Count
            };
            return Json(gridModel);
        }
	}
}