using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.API.Models
{
    public class AchievementCategoryDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AchievementDto> Achievements { get; set; } = new List<AchievementDto>();
    }
}
