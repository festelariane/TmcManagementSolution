using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Tmc.Admin.Models.CardTypes;
using Tmc.Admin.Models.Customers;

namespace Tmc.Admin.Validators
{
    public class DepositTransactionValidator : AbstractValidator<DepositTransactionModel>
    {
        public DepositTransactionValidator()
        {
            RuleFor(c => c.Amount).NotEmpty().WithMessage("Amount is required").GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}