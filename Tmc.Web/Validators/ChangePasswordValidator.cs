using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Models.Customers;

namespace Tmc.Web.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(o => o.OldPassword).NotEmpty().WithMessage("Old password is required");
            RuleFor(o => o.NewPassword).NotEmpty().WithMessage("New password is required");
            RuleFor(o => o.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required");
            RuleFor(o => o.ConfirmPassword).Equal(x => x.NewPassword).WithMessage("Password doesn't match");
        }
    }
}