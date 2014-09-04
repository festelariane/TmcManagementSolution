using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Framework.Models;
using Tmc.Web.Validators;

namespace Tmc.Web.Models.Customers
{
    [Validator(typeof(LoginValidator))]
    public class LoginModel : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool DisplayCaptcha { get; set; }
    }
}