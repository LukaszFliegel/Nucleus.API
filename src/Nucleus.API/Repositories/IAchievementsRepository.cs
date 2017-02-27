using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nucleus.API.Entities;

namespace Nucleus.API.Repositories
{
    public interface IAchievementsRepository: ICrudRepository<Achievement>
    {
        IEnumerable<Achievement> GetAll(bool includeCategory);
    }
}
