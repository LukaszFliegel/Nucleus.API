using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nucleus.API.Repositories
{
    public interface ICrudRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetOne(int id);

        void Add(T achievement);

        bool Delete(int id);

        bool SaveChanges();
    }
}
