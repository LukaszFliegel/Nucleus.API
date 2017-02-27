using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Nucleus.API.Entities;

namespace Nucleus.API.Validators
{
    public class AchievementValidator: AbstractValidator<Achievement>
    {
        public AchievementValidator()
        {
            RuleFor(x => x.Name).NotEqual(p => p.Description).WithMessage("Name and Description can not be the same");
        }
    }
}
