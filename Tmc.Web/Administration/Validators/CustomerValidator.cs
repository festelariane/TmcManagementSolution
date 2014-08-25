using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Tmc.Admin.Models.CardTypes;
using Tmc.Admin.Models.Customers;

namespace Tmc.Admin.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
}