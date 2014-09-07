using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.BLL.Contract.Authentication;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Enums;
using Tmc.Web.Models.Customers;

namespace Tmc.Web.Controllers
{
    public class CustomerController : BaseFrontEndController
    {
        private readonly IAuthenticationBiz _authenticationBiz;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        public CustomerController(IAuthenticationBiz authenticationBiz, ICustomerRegistrationService customerRegistrationService)
        {
            this._authenticationBiz = authenticationBiz;
            this._customerRegistrationService = customerRegistrationService;
        }
        public ActionResult Login()
        {
            var model = new LoginModel();
            model.RememberMe = true;
            model.DisplayCaptcha = true;
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Customer customer = null;
                var loginResult = _customerRegistrationService.ValidateUserForLogin(model.UserName, model.Password, out customer) ;
                switch (loginResult)
                {
                    case LoginResult.Successful:
                        {
                            //sign in new customer
                            _authenticationBiz.SignIn(customer, model.RememberMe);

                            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);
                            else
                                return RedirectToRoute("HomePage");
                        }
                    case LoginResult.CustomerNotExist:
                        ModelState.AddModelError("", "User doesn't exist");
                        break;
                    
                    case LoginResult.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Wrong credentials");
                        break;
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationBiz.SignOut();
            return RedirectToRoute("HomePage");
        }
    }
}