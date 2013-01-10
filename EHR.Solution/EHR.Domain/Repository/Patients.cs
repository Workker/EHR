using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using NHibernate;

namespace EHR.Domain.Repository
{
    public class Patients : BaseRepository
    {
        public Patients() { }
        public Patients(ISession session) : base(session) { }

        public virtual Patient GetBy(int id)
        {
            return base.Get<Patient>(id);
        }
    }
}
