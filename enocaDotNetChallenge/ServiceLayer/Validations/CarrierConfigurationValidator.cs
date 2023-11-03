using enocaDotNetChallenge.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Service.Validations
{
    public class CarrierConfigurationValidator : AbstractValidator<CarrierConfigurations>
    {
        public CarrierConfigurationValidator()
        {
            RuleFor(x => x.CarrierMinDesi).InclusiveBetween(0, int.MaxValue).WithMessage("{ PropertyName } can't be less than 0");
            RuleFor(x => x.CarrierMaxDesi).InclusiveBetween(1, int.MaxValue).WithMessage("{ PropertyName } must be greater than 0");
            RuleFor(x => x.CarrierCost).InclusiveBetween(0, int.MaxValue).WithMessage("{ PropertyName } can't be less than 0");
        }
    }
}
