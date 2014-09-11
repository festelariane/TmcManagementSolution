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
    [Validator(typeof(CustomerValidator))]
    public class CustomerModel : BaseEntityModel
    {
        public CustomerModel()
        {
            CardType = new CardTypeModel();
        }
        public string CustomerCode { get; set; }
        public int? CardTypeId { get; set; }
        public CardTypeModel CardType { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public decimal Points { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
    }
}