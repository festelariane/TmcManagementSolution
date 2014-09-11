using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Admin.Models.CardTypes;
using Tmc.Admin.Validators;
using Tmc.Web.Framework.Models;

namespace Tmc.Admin.Models.Customers
{
    public class ApplyCustomerRoleModel : BaseModel
    {
        public ApplyCustomerRoleModel()
        {
        }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}