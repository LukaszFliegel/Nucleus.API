using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nucleus.API.Entities;

namespace Nucleus.API.Repositories
{
    public class AchievementsSqlRepository : IAchievementsRepository
    {
        private NucleusDbContext _context;

        public AchievementsSqlRepository(NucleusDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Achievement> GetAchievements(bool includeCategory = false)
        {
            if (includeCategory)
            {
                return _context.Achievements.Include(p => p.Category).OrderBy(p => p.Name).ToList();
            }

            return _context.Achievements.OrderBy(p => p.Name).ToList();
        }

        public Achievement GetAchievement(int id)
        {
            return _context.Achievements.FirstOrDefault(p => p.Id == id);
        }

        public void AddAchievement(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
        }

        public bool DeleteAchievement(int id)
        {
            var achievementToDelete = GetAchievement(id);

            if(achievementToDelete != null)
            {
                _context.Achievements.Remove(achievementToDelete);

                return true;
            }

            return false;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
