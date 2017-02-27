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

        public IEnumerable<Achievement> GetAll()
        {
            return GetAll(false);
        }

        public IEnumerable<Achievement> GetAll(bool includeCategory = false)
        {
            if (includeCategory)
            {
                return _context.Achievements.Include(p => p.Category).OrderBy(p => p.Name).ToList();
            }

            return _context.Achievements.OrderBy(p => p.Name).ToList();
        }

        public Achievement GetOne(int id)
        {
            return _context.Achievements.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
        }

        public bool Delete(int id)
        {
            var achievementToDelete = GetOne(id);

            if(achievementToDelete == null)
            {
                return false;
            }

            _context.Achievements.Remove(achievementToDelete);

            return false;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
