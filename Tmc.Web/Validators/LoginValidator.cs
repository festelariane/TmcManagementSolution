using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Models.Customers;

namespace Tmc.Web.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(o => o.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(o => o.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}