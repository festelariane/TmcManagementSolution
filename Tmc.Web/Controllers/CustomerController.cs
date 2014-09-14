using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.BLL.Contract.Authentication;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Enums;
using Tmc.Web.Framework.FilterAttributes;
using Tmc.Web.Models.Customers;
using Tmc.Core.Domain.Extensions;
using Tmc.Web.Extensions;

namespace Tmc.Web.Controllers
{
    public class CustomerController : BaseFrontEndController
    {
        private readonly IWorkContext _workContext;
        private readonly IAuthenticationBiz _authenticationBiz;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        public CustomerController(IWorkContext workContext, IAuthenticationBiz authenticationBiz, ICustomerRegistrationService customerRegistrationService)
        {
            this._workContext = workContext;
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

        [AdminAuthorize("RegisteredUsers, Administrators")]
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }
        [HttpPost]
        [AdminAuthorize("RegisteredUsers, Administrators")]
       
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            model.ErrorMessage.Clear();
            var customer = _workContext.CurrentCustomer;
            var result = _customerRegistrationService.ChangePassword(customer.Id,model.OldPassword, model.NewPassword);
            model.Success = result == ChangePasswordResult.Successful;
            switch(result)
            {
                case ChangePasswordResult.Successful:
                    model.Success = true;
                    break;
                case ChangePasswordResult.CustomerNotExist:
                    model.ErrorMessage.Add("Customer not found");
                    break;
                case ChangePasswordResult.OldPasswordNotValid:
                    model.ErrorMessage.Add("Old password not valid");
                    break;
                default:
                    model.ErrorMessage.Add("Cannot change password. Please try again later");
                    break;
            }
            return View(model);
        }

        [AdminAuthorize("RegisteredUsers")]
        public ActionResult Dashboard()
        {
            var model = new CustomerDashboardModel();
            var currentCustomer = _workContext.CurrentCustomer;

            var customerInfoModel = new CustomerInfoModel();
            var cardType = currentCustomer.CardType;
            customerInfoModel.CardType = cardType != null ? cardType.Name : string.Empty;
            customerInfoModel.CustomerCode = (cardType != null ? cardType.Prefix : string.Empty) + currentCustomer.CustomerCode;
            customerInfoModel.FullName = currentCustomer.FullName;
            customerInfoModel.CreatedOnUtc = currentCustomer.CreatedOnUtc;
            customerInfoModel.UpdatedOnUtc = currentCustomer.UpdatedOnUtc;
            customerInfoModel.LastActivityDateUtc = currentCustomer.LastActivityDateUtc;
            
            model.CustomerInfo = customerInfoModel;
            return View(model);
        }
    }
}