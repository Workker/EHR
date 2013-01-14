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
        #region Properties

        public Patients() { }
        public Patients(ISession session) : base(session) { }

        #endregion

        #region Methods

        public virtual Patient GetBy(int id)
        {
            return base.Get<Patient>(id);
        }

        #endregion
    }
}
