using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Tmc.Admin.Models.CardTypes;

namespace Tmc.Admin.Validators
{
    public class CardTypeValidator : AbstractValidator<CardTypeModel>
    {
        public CardTypeValidator()
        {
            RuleFor(ct => ct.Name).NotEmpty().WithMessage("CardType is required");
        }
    }
}