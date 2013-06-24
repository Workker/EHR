using System.Collections.Generic;

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
