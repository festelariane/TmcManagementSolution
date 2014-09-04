using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Web.Models.Customers;

namespace Tmc.Web.Controllers
{
    public class CustomerController : BaseFrontEndController
    {
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {

            }
            return null;
        }
    }
}