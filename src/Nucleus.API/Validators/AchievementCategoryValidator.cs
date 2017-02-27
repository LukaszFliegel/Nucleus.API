using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nucleus.API.Entities;

namespace Nucleus.API.Validators
{
    public class AchievementCategoryValidator : AbstractValidator<AchievementCategory>
    {
        public AchievementCategoryValidator()
        {
            RuleFor(x => x.Name).NotEqual(p => p.Description).WithMessage("Name and Description can not be the same");
        }
    }
}
