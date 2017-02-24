using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.API.Entities;

namespace Nucleus.API.Repositories
{
    public interface IAchievementsRepository
    {
        IEnumerable<Achievement> GetAchievements(bool includeCategory);

        Achievement GetAchievement(int id);

        void AddAchievement(Achievement achievement);

        bool DeleteAchievement(int id);

        bool SaveChanges();
    }
}
