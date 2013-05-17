using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Repository
{
    public class Types<T> : BaseRepository
    {
        public T Get(short id)
        {
            return Session.Get<T>(id);
        }

        public IList<T> All()
        {
            return All<T>();
        }


    }
}
