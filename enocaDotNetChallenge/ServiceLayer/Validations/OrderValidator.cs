using enocaDotNetChallenge.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Service.Validations
{
    public class OrderValidator : AbstractValidator<Orders>
    {
        public OrderValidator() 
        {
            RuleFor(x => x.OrderDesi).InclusiveBetween(1, int.MaxValue).WithMessage("{ PropertyName } must be greater than 0");
            RuleFor(x => x.OrderCarrierCost).InclusiveBetween(0, int.MaxValue).WithMessage("{ PropertyName } can't be less than 0");
        }
    }
}
