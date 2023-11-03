using enocaDotNetChallenge.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Service.Validations
{
    public class CarrierValidator : AbstractValidator<Carriers>
    {
        public CarrierValidator() 
        {
            RuleFor(x=>x.CarrierName).NotNull().WithMessage("{ PropertyName } is required").NotEmpty().WithMessage("{ PropertyName } is required");
            RuleFor(x => x.CarrierIsActive).Must(x => x == true || x == false).WithMessage("{ ProperyName } field can only be true or false.");
            RuleFor(x => x.CarrierPlusDesiCost).InclusiveBetween(0, int.MaxValue).WithMessage("{ PropertyName } can't be less than 0");
        }
    }
}
