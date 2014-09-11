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
    [Validator(typeof(WithdrawTransactionValidator))]
    public class WithdrawTransactionModel : BaseEntityModel
    {
        public WithdrawTransactionModel()
        {
            Customer = new CustomerModel();
        }
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal Points { get; set; }
        public string Reason { get; set; }
    }
}