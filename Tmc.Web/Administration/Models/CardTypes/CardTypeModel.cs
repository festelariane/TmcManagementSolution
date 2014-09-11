using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Admin.Validators;
using Tmc.Web.Framework.Models;

namespace Tmc.Admin.Models.CardTypes
{
    [Validator(typeof(CardTypeValidator))]
    public class CardTypeModel : BaseEntityModel
    {
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public decimal Threshold { get; set; }
        public int DisplayOrder { get; set; }
        public int Level { get; set; }
        public string Prefix { get; set; }
    }
}