using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.API.Entities;

namespace Nucleus.API.Repositories
{
    public interface IAchievementCategoryRepository
    {
        IEnumerable<AchievementCategory> GetAchievementCategories();

        AchievementCategory GetAchievementCategory(int id);

        void AddAchievementCategory(AchievementCategory achievement);

        bool DeleteAchievementCategory(int id);

        bool SaveChanges();
    }
}
