using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using NHibernate;

namespace EHR.Domain.Repository
{
    public class Diagnostics : BaseRepository
    {
        public Diagnostics() { }
        public Diagnostics(ISession session) : base(session) { }

        public virtual void Save(Diagnostic diagnostic)
        {
            base.Save(diagnostic);
        }

        public virtual Diagnostic GetBy(int id)
        {
            return base.Get<Diagnostic>(id);
        }

    }
}
