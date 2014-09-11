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
using Tmc.Admin.Models.Customers;

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

        public ActionResult ApplyCustomerRoles(int customerId)
        {
            if(customerId <= 0)
            {
                return Content("");
            }
            var allRoles = _customerBiz.GetAllCustomerRoles();            
            var customer = _customerBiz.GetCustomerById(customerId);
            if(customer == null)
            {
                return Content("");
            }
            var model = PrepareApplyCustomerRoleModel(allRoles, customer);

            return PartialView("_ApplyCustomerRoles", model);
        }
        private IList<ApplyCustomerRoleModel> PrepareApplyCustomerRoleModel(IList<CustomerRole> allRoles, Customer customer)
        {
            var retVal = new List<ApplyCustomerRoleModel>();
            var customerRoles = customer.CustomerRoles.ToList();
            if (allRoles != null)
            {
                foreach (var role in allRoles)
                {
                    var model = new ApplyCustomerRoleModel();
                    model.CustomerId = customer.Id;
                    model.CustomerCode = customer.CustomerCode;
                    model.CustomerName = customer.UserName;
                    model.RoleName = role.Name;
                    
                    if (customerRoles.Contains(role))
                    {
                        model.Selected = true;
                    }
                    else
                    {
                        model.Selected = false;
                    }
                    retVal.Add(model);
                }
            }
            return retVal;
        }
	}
}