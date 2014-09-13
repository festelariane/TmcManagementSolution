using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Framework.Models;
using Tmc.Web.Validators;

namespace Tmc.Web.Models.Customers
{
    [Validator(typeof(ChangePasswordValidator))]
    public class ChangePasswordModel : BaseModel
    {
        public ChangePasswordModel()
        {
            ErrorMessage = new List<string>();
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public bool Success { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}