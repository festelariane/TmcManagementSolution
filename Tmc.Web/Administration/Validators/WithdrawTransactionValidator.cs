using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Tmc.Admin.Models.CardTypes;
using Tmc.Admin.Models.Customers;

namespace Tmc.Admin.Validators
{
    public class WithdrawTransactionValidator : AbstractValidator<WithdrawTransactionModel>
    {
        public WithdrawTransactionValidator()
        {
            RuleFor(c => c.Points).NotEmpty().WithMessage("Points is required").GreaterThan(0).WithMessage("Points must be greater than 0");
        }
    }
}