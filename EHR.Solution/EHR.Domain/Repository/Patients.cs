using EHRIntegracao.Domain.Domain;
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