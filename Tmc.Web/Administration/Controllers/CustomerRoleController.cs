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
        [AdminAuthorize]
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
        [AdminAuthorize]
        [HttpPost]
        public ActionResult ApplyCustomerRoles(ApplyCustomerRoleModel model)
        {
            if (model.CustomerId <= 0)
            {
                return Content("");
            }
            bool bOk = _customerBiz.AssignUserToRoles(model.CustomerId, model.AllRoles.Where(cr => cr.Selected).Select(m => m.Id).ToList());
            return Json(new { Success = bOk });
        }
        private ApplyCustomerRoleModel PrepareApplyCustomerRoleModel(IList<CustomerRole> allRoles, Customer customer)
        {
            var retVal = new ApplyCustomerRoleModel();
            retVal.CustomerId = customer.Id;
            retVal.CustomerCode = customer.CustomerCode;
            retVal.CustomerName = customer.UserName;

            var customerRoles = customer.CustomerRoles.ToList();
            if (allRoles != null)
            {
                foreach (var role in allRoles)
                {
                    var model = new CustomerRoleModel();
                    
                    model.Name = role.Name;
                    model.Id = role.Id;

                    if (customerRoles.Contains(role))
                    {
                        model.Selected = true;
                    }
                    else
                    {
                        model.Selected = false;
                    }
                    retVal.AllRoles.Add(model);
                }
            }
            return retVal;
        }
	}
}