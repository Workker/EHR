using EHRIntegracao.Domain.Domain;
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