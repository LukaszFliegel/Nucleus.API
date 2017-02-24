using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nucleus.API.Entities
{
    public class NucleusDbContext: DbContext
    {
        public NucleusDbContext(DbContextOptions options)
            :base(options)
        {
            Database.Migrate();
        }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<AchievementCategory> AchievementsCategories { get; set; }
    }
}
