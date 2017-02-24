using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.API.Entities;

namespace Nucleus.API.Repositories
{
    public class AchievementCategorySqlRepository : IAchievementCategoryRepository
    {
        private NucleusDbContext _context;

        public AchievementCategorySqlRepository(NucleusDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AchievementCategory> GetAchievementCategories()
        {
            return _context.AchievementsCategories.OrderBy(p => p.Name).ToList();
        }

        public AchievementCategory GetAchievementCategory(int id)
        {
            return _context.AchievementsCategories.Where(p => p.Id == id).FirstOrDefault();
        }

        public void AddAchievementCategory(AchievementCategory achievement)
        {
            _context.Add(achievement);
        }

        public bool DeleteAchievementCategory(int id)
        {
            var achievementToDelete = GetAchievementCategory(id);

            if(achievementToDelete == null)
            {
                return false;
            }

            _context.Remove(achievementToDelete);

            return true;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
